using FluentValidation;
using HaberApp.Core.DTOs.RequestDtos;

namespace HaberApp.ServiceLayer.Validations
{
    public class RoleRequestValidator : AbstractValidator<CreateRoleRequestDto>
    {
        private const string RoleEmptyError = "Rol Adını Girmelisiniz...";
        private const string CountError = "Yetki Alanları Boş Bırakılamaz...";
        public RoleRequestValidator()
        {
            RuleFor(a => a.RoleName).NotEmpty().WithMessage(RoleEmptyError)
                .NotNull().WithMessage(RoleEmptyError);

            RuleFor(a => a.PermissionList)
                .NotNull().WithMessage(CountError)
                .NotEmpty().WithMessage(CountError).DependentRules(() =>
                {
                    RuleFor(a => a.PermissionList.Count > 0);
                }).WithMessage(CountError);


        }
    }
}
