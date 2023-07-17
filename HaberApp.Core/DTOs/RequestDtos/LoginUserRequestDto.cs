namespace HaberApp.Core.DTOs.RequestDtos
{
    public class LoginUserRequestDto : BaseRequestDto
    {
        public string EMail { get; set; }
        public string Password { get; set; }
    }
}
