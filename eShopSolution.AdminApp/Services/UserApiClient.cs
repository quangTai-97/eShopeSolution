using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<string> Authenticate(LoginRequest request)
        {
            //POST
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PostAsync("/api/users/authenticate/", httpContent);

            //Get token;
            var token =await response.Content.ReadAsStringAsync();
            return token;
        } 

        public async Task<PagedResult<UserViewModel>> GetUserPaging(GetUserPagingRequest request)
        {
        
           

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",request.BearerToken);
               
            var response = await client.GetAsync($"/api/Users/paging?pageIndex=" +
                $"{request.pageIndex}&pageSize={request.pageSize}&keyword={request.Keyword}");
            var body = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<PagedResult<UserViewModel>>(body);
            return users;
        }

        public async Task<bool> RegisterUser(RegisterRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PostAsync("/api/users/register", httpContent);

            //Get token;
            //var token = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }
    }
}
