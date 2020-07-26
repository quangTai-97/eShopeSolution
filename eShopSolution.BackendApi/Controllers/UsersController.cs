using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.Application.System;
using eShopSolution.ViewModels.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        //chưa đăng nhập vẫn cho vào
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody]LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var resultToken =await _userService.Authencate(request);
            if(string.IsNullOrEmpty(resultToken.Message))
            {
                return BadRequest("userName or password is incorrect");
            }
           
            return Ok( resultToken);
        }

        [HttpPost("register")]
        //chưa đăng nhập vẫn cho vào
        [AllowAnonymous]
        public async Task<IActionResult> Resgister([FromBody]RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
          
            var result = await _userService.Register(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        //http://localhost/api/users/paging?pageIndex=1&pageSize=10&keyword=
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]GetUserPagingRequest requets)
        {
            var products = await _userService.GetUserPaging(requets);
            //trả về 200
            return Ok(products);
        }

        
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody]UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Update(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        //GET: http://localhost/api/users?id=
        [HttpGet("users")]
        public async Task<IActionResult> Update(Guid Id)
        {
            var result = await _userService.GetUserById(Id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        //DELETE: http://localhost/api/delete?id=
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _userService.Delete(Id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}