  using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.System
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser>  _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        public UserService(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }
        public async Task<ApiResult<string>> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return new ApiErrorResult<string>("Tài khoản không tồn tại");
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RemmemberMe, true);
            if (!result.Succeeded)
                return null;

            var roles =await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Name,request.UserName),
                new Claim(ClaimTypes.Role,string.Join(";",roles)),
            };
            //Generate key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            var tokenRead =  new JwtSecurityTokenHandler().WriteToken(token);
            return new ApiResult<string>()
            {
                Message = tokenRead
            }; 

        }

        public async Task<ApiResult<PagedResult<UserViewModel>>> GetUserPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword) || x.PhoneNumber.Contains(request.Keyword) || x.LastName.Contains(request.Keyword));
            }


            // 3.Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.pageIndex - 1) * request.pageSize)
                .Take(request.pageSize)
                .Select(x => new UserViewModel()
                {
                   Email = x.Email,
                   PhoneNumber = x.PhoneNumber,
                   UserName = x.UserName,
                   FirstName = x.FirstName,
                   Id = x.Id,
                   LasttName = x.LastName

                }).ToListAsync();

            // 4.Select  and projection
            var pagedResult = new PagedResult<UserViewModel>()
            {
                TotalRecord = totalRow,
                PageSize = request.pageSize,
                PageIndex = request.pageIndex,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<UserViewModel>>(pagedResult);
        }

        public async Task<ApiResult<UserUpdateRequest>> GetUserById(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var roles = await _userManager.GetRolesAsync(user);
            var result = new UserUpdateRequest()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Dob = user.Dob,
                Id = userId,
                Username = user.UserName,
                RoleAssign = roles
                
            };
            return new ApiSuccessResult<UserUpdateRequest>(result);
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if(user != null)
            {
                return new ApiErrorResult<bool>("UserName đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }    

                user = new AppUser()
            {
                Dob = request.Dob,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName,

            };
            
            var result = await _userManager.CreateAsync(user, request.Password);
            if(result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Đăng kí không thành công");
        }

        public async Task<ApiResult<bool>> Update(UserUpdateRequest request)
        {
           // var user = await _userManager.FindByNameAsync(request.UserName);

            if (await _userManager.Users.AnyAsync(x=>x.Email ==  request.Email && x.Id !=  request.Id))
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            user.Dob = request.Dob;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;


            

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công");
        }

        public async Task<ApiResult<bool>> Delete(Guid userId)
        {
            var user = _userManager.FindByIdAsync(userId.ToString());
            if(user == null)
            {
                return new ApiErrorResult<bool>("User không tồn tại");
            }    
            var result = await _userManager.DeleteAsync(user.Result);
            if(result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Xóa không thành công");

        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            var removeRoles = request.Roles.Where(x => x.Selected == false).Select(x=>x.Name).ToList();
            foreach (var roleName in removeRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }
           // await _userManager.RemoveFromRolesAsync(user, removeRoles);

            var AddedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach(var roleName in AddedRoles)
            {
                if( await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }    
            }    
                return new ApiSuccessResult<bool>();
            
           
        }
    }
}
