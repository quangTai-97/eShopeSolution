using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.Application.System.Role;
using eShopSolution.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        public readonly IRoleServiec _roleServiec;
        public RolesController(IRoleServiec roleServiec)
        {
            _roleServiec = roleServiec;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleServiec.GetAll();
            return Ok(roles);
        }
    }
}