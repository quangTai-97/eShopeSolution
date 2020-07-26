using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.System
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authencate(LoginRequest request);

        Task<ApiResult<bool>> Register(RegisterRequest request);

        Task<ApiResult<PagedResult<UserViewModel>>> GetUserPaging(GetUserPagingRequest request);

        Task<ApiResult<UserUpdateRequest>> GetUserById(Guid userId);
        Task<ApiResult<bool>> Update(UserUpdateRequest request);

        Task<ApiResult<bool>> Delete(Guid userId);

        Task<ApiResult<bool>> RoleAssign(Guid id,RoleAssignRequest request);


    }
}
