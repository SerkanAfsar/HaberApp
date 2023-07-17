using FluentValidation;
using HaberApp.Core.DTOs.RequestDtos;

namespace HaberApp.ServiceLayer.Validations
{
    public class UserLoginRequestDtoValidator : AbstractValidator<LoginUserRequestDto>
    {
        public UserLoginRequestDtoValidator()
        {
            RuleFor(a => a.EMail)
             .NotEmpty().WithMessage("E-Posta Giriniz")
             .NotNull().WithMessage("E-Posta Giriniz").DependentRules(() =>
             {
                 RuleFor(a => a.EMail).EmailAddress().WithMessage("E-Posta Formatı Yanlış!");
             });

            RuleFor(a => a.Password).NotNull().WithMessage("Şifre Giriniz").NotEmpty().WithMessage("Şifre Giriniz");
        }
    }
}
