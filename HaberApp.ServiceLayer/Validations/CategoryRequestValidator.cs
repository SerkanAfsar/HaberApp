using FluentValidation;
using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.Repositories;

namespace HaberApp.ServiceLayer.Validations
{
    public class CategoryRequestValidator : AbstractValidator<CategoryRequestDto>
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryRequestValidator(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;

            RuleFor(a => a.CategoryName)
           .NotNull().WithMessage("Kategori Adı Boş Bırakılamaz...")
           .NotEmpty().WithMessage("Kategori Adı Boş Bırakılamaz...").DependentRules(() =>
           {
               RuleFor(a => a.CategoryName).MustAsync(async (name, CancellationToken) =>
               {
                   var result = await categoryRepository.DoesExistCategory(name, CancellationToken);
                   return !result;
               }).WithMessage("Girdiğiniz Kategori Adı Sistemde Bulunmaktadır");
           });

        }

    }
}
