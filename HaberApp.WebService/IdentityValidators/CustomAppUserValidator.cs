using HaberApp.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace HaberApp.WebService.IdentityValidators
{
    public class CustomAppUserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var errors = new List<IdentityError>();
            var isDigit = int.TryParse(user.UserName[0].ToString(), out _);
            if (isDigit)
            {
                errors.Add(new IdentityError() { Code = "FirstDigitError", Description = "Kullanıcı Adının İlk Harfi Rakam Olamaz" });
            }
            if (!user.UserName.Contains("@"))
            {
                errors.Add(new IdentityError() { Code = "NotEMail", Description = "Kullanıcı E-Posta Formatı Yanlış" });
            }
            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
