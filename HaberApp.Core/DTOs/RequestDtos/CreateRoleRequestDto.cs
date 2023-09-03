namespace HaberApp.Core.DTOs.RequestDtos
{
    public class CreateRoleRequestDto : BaseRequestDto
    {
        public string RoleName { get; set; }
        public List<PermissionList> PermissionList { get; set; }

    }
    public class PermissionList
    {
        public string PermissionValue { get; set; }

    }
}
