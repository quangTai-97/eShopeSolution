using eShopSolution.ViewModels.System.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.System.Role
{
    public interface IRoleServiec
    {
        Task<List<RoleVM>> GetAll();
    }
}
