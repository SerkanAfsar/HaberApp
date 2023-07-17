using Microsoft.AspNetCore.Identity;

namespace HaberApp.Core.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? ProfilePicture { get; set; }
    }
}
