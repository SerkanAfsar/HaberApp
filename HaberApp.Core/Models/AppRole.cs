using HaberApp.Core.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace HaberApp.Core.Models
{
    public class AppRole : IdentityRole
    {
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public RoleTypes RoleType { get; set; }
    }
}
