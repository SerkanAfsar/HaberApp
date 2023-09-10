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
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRole(string roleId, CancellationToken cancellationToken = default)
        {
            return Ok(await this.roleService.GetRoleAsync(roleId, cancellationToken));
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles(CancellationToken cancellationToken = default)
        {
            return Ok(await this.roleService.GetRolesAsync(cancellationToken));
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequestDto model)
        {
            return Ok(await this.roleService.CreateRoleWithClaimsAsync(model));
        }
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            return Ok(await this.roleService.DeleteRoleAsync(roleId));
        }
    }
}
