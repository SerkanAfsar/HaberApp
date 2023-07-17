using FluentValidation;
using HaberApp.Core.DTOs.RequestDtos;

namespace HaberApp.ServiceLayer.Validations
{
    public class CreateUserRequestDtoValidator : AbstractValidator<CreateUserRequestDto>
    {
        public CreateUserRequestDtoValidator()
        {
            RuleFor(a => a.EMail)
                .NotEmpty().WithMessage("E-Mail Boş Bırakılamaz")
                .NotNull().WithMessage("E-Mail Boş Bırakılamaz")
                .DependentRules(() =>
                {
                    RuleFor(a => a.EMail).EmailAddress().WithMessage("E-Posta Formatı Yanlış");

                });

            RuleFor(a => a.Password).NotNull().WithMessage("Şifre Boş Bırakılamaz")
                .NotEmpty().WithMessage("Şifre Boş Bırakılamaz");

        }
    }
}
