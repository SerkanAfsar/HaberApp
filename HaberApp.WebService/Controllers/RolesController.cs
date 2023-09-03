using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models.Abstract;
using HaberApp.Core.Services;
using HaberApp.WebService.CustomFilters;
using Microsoft.AspNetCore.Mvc;

namespace HaberApp.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CustomFilterAttribute<BaseEntity, CreateRoleRequestDto, RoleResponseDto>))]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService roleService;
        public RolesController(IRoleService roleService)
        {
            this.roleService = roleService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequestDto model)
        {
            return Ok(await this.roleService.CreateRoleWithClaimsAsync(model));
        }
    }
}
