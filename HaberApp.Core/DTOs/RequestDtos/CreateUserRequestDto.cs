namespace HaberApp.Core.DTOs.RequestDtos
{
    public class CreateUserRequestDto : BaseRequestDto
    {
        public string EMail { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int RoleType { get; set; }
    }
}
