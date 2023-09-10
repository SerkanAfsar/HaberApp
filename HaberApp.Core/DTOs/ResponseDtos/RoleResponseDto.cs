using HaberApp.Core.DTOs.RequestDtos;

namespace HaberApp.Core.DTOs.ResponseDtos
{
    public class RoleResponseDto : BaseResponseDto
    {
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public List<PermissionList> PermissionList { get; set; }
    }
}
