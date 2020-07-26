using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using eShopSolution.AdminApp.Services;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace eShopSolution.AdminApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        private readonly IRoleApiClient _roleApiClient;
        public UserController(IUserApiClient userApiClient,IConfiguration configuration,IRoleApiClient roleApiClient)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
            _roleApiClient = roleApiClient;
        }
        public async Task<IActionResult> Index(string keyWord,int pageIndex = 1, int pageSize = 1)
        {
            var sessoion = HttpContext.Session.GetString("Token");
            var requset = new GetUserPagingRequest()
            {
                BearerToken = sessoion,
                Keyword = keyWord,
                pageIndex = pageIndex,
                pageSize = pageSize
            };
            var data = await _userApiClient.GetUserPaging(requset);
            ViewBag.KeyWord = keyWord;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }    
            return View(data.ResultObject);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View(ModelState);
            var result = await _userApiClient.RegisterUser(request);
            if(result.IsSuccessed)
            {
                TempData["result"] = "success";
                return RedirectToAction("Index");
            }    
            ModelState.AddModelError("", result.Message);
            return View(request);
            
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View(ModelState);
            var result = await _userApiClient.UpdateUser(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "success";
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);

        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid userId)
        {
            //var user = _userApiClient
            var result = await _userApiClient.GetUserById(userId);
            if (result.IsSuccessed)
                return View(result.ResultObject);
            return RedirectToAction("Index", "User");

        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            var result = await _userApiClient.GetUserById(Id);
            return View(result.ResultObject);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            return View(new UserDeleteRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View(ModelState);
            var result = await _userApiClient.DeleteUser(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "success";
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);

        }

        [HttpGet]
        public async Task<IActionResult> RoleAssign(Guid id)
        {
            var roleAssignRequest = await GetRoleAssignRequest(id);

                return View(roleAssignRequest);
            

        }

        [HttpPost]
        public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return View(ModelState);
            var result = await _userApiClient.RoleAssign(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật quyền thành công";
                return RedirectToAction("Index", "User");
            }
            ModelState.AddModelError("", result.Message);
            var roleAssignRequest =await GetRoleAssignRequest(request.Id);

            return View(roleAssignRequest);
          //  return View(request);

        }
        private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid id)
        {
            var result = await _userApiClient.GetUserById(id);
            var roles = await _roleApiClient.GetAll();
            var roleAssignRequest = new RoleAssignRequest();
            List<SelectItem> RolesAdd = new List<SelectItem>();
            foreach (var roleName in roles.ResultObject)
            {
                
                SelectItem addSelec = new SelectItem()
                {
                    Id = roleName.Id.ToString(),
                    Name = roleName.Name,
                    Selected = result.ResultObject.RoleAssign.Contains(roleName.Name)

                };
                RolesAdd.Add(addSelec);
                roleAssignRequest.Id = roleName.Id;
               


                roleAssignRequest.Roles = RolesAdd;

            }
            return roleAssignRequest;
        }

        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }

    }
}