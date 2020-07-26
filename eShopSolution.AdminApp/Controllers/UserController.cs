using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using eShopSolution.AdminApp.Services;
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
        public UserController(IUserApiClient userApiClient,IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index(string keyWord,int pageIndex = 1, int pageSize = 10)
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
                return RedirectToAction("Index", "User");

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
                return RedirectToAction("Index", "User");
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