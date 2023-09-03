using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Utils;

namespace HaberApp.Core.Services
{
    public interface IRoleService
    {
        Task<ResponseResult<RoleResponseDto>> CreateRoleWithClaimsAsync(CreateRoleRequestDto requestDto);
    }
}
