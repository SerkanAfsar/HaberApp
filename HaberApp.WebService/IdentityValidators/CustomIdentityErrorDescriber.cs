using Microsoft.AspNetCore.Identity;

namespace HaberApp.WebService.IdentityValidators
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        private readonly string existString = "Sistemde Kayıtlı {0} bulunmaktadır";


        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError { Code = "DuplicateEmail", Description = string.Format(existString, email) };
        }
        public override IdentityError DuplicateRoleName(string role)
        {
            return new IdentityError { Code = "DuplicateRole", Description = string.Format(existString, role) };
        }
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError { Code = "DuplicateUserName", Description = string.Format(existString, userName) };
        }
        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError() { Code = "InvalidEmail", Description = "EMail Formatı Yanlış" };
        }
        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError() { Code = "InvalidUserName", Description = "Kullanıcı Adı  Formatı Yanlış" };
        }
    }
}
