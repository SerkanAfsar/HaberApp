using Microsoft.AspNetCore.Identity;

namespace HaberApp.Core.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ProfilePicture { get; set; }
    }
}
