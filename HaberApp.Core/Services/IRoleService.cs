using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Utils;

namespace HaberApp.Core.Services
{
    public interface IRoleService
    {
        Task<ResponseResult<RoleResponseDto>> CreateRoleWithClaimsAsync(CreateRoleRequestDto requestDto, CancellationToken cancellationToken = default);
        Task<ResponseResult<RoleResponseDto>> GetRolesAsync(CancellationToken cancellationToken = default);
        Task<ResponseResult<RoleResponseDto>> DeleteRoleAsync(string roleId, CancellationToken cancellationToken = default);
        Task<ResponseResult<RoleResponseDto>> GetRoleAsync(string roleId, CancellationToken cancellationToken = default);
    }
}
