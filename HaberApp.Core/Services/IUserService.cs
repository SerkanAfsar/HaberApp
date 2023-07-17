using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.Utils;

namespace HaberApp.Core.Services
{
    public interface IUserService
    {
        Task<ResponseResult<string>> CreateAppUserUserAsync(CreateUserRequestDto model);
        Task<ResponseResult<string>> LoginUser(LoginUserRequestDto model);
    }
}
