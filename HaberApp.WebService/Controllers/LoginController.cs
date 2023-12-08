using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models.Abstract;
using HaberApp.Core.Services;
using HaberApp.ServiceLayer.Constants;
using HaberApp.WebService.CustomFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HaberApp.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CustomFilterAttribute<BaseEntity, CreateUserRequestDto, CreateUserResponseDto>))]

    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        public LoginController(IUserService _userService)
        {
            this._userService = _userService;
        }

        [CustomAuthorize(Modules.SiteSettings.Create)]
        [HttpPost("CreateUser")]
        public async Task<IActionResult> AddUser([FromBody] CreateUserRequestDto model)
        {
            var result = await this._userService.CreateAppUserUserAsync(model);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        [ServiceFilter(typeof(CustomFilterAttribute<BaseEntity, LoginUserRequestDto, LoginUserResponseDto>))]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequestDto loginUser)
        {
            var result = await this._userService.LoginUser(loginUser);
            return Ok(result);
        }
    }
}
