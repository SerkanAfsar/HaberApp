using FluentValidation;
using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.Models.Enums;
using HaberApp.Core.Repositories;

namespace HaberApp.ServiceLayer.Validations
{
    public class CategorySourceRequestValidator : AbstractValidator<CategorySourceRequestDto>
    {
        private readonly ICategorySourceRepository categorySourceRepository;
        private readonly ICategoryRepository categoryRepository;
        public CategorySourceRequestValidator(ICategorySourceRepository categorySourceRepository, ICategoryRepository categoryRepository)
        {
            this.categorySourceRepository = categorySourceRepository;
            this.categoryRepository = categoryRepository;

            RuleFor(a => a.SourceUrl)
                .NotNull().WithMessage("Kaynak Url Boş Bırakılamaz")
                .NotEmpty().WithMessage("Kaynak Url Boş Bırakılamaz").DependentRules(() =>
                {
                    RuleFor(a => a.SourceUrl).MustAsync(async (a, CancellationToken) =>
                    {
                        var result = await this.categorySourceRepository.GetByFilterAsync(b => b.SourceUrl.ToLower().Trim() == a.ToLower().Trim(), CancellationToken) ?? null;
                        return result == null;
                    }).WithMessage("Girdiğiniz Kaynak Url'ye ait kayıt mevcut");
                });


            RuleFor(a => a.CategoryId)
                .NotNull().WithMessage("Kategori Seçiniz")
                .NotEmpty().WithMessage("Kategori Seçiniz").DependentRules(() =>
                {
                    RuleFor(a => a.CategoryId).MustAsync(async (a, CancellationToken) =>
                    {
                        var result = await this.categoryRepository.GetByIdAsync(a, CancellationToken) ?? null;
                        return result != null;
                    }).WithMessage("Kategori Seçimi Yanlış");
                });


            RuleFor(a => a.SourceType)
                .NotNull().WithMessage("Kaynak Tipi Seçiniz")
                .NotEmpty().WithMessage("Kaynak Tipi Seçiniz").DependentRules(() =>
                {
                    RuleFor(a => a.SourceType).Must(a =>
                    {
                        var hasValue = false;
                        var list = Enum.GetValues(typeof(NewsSource));
                        foreach (var item in list)
                        {
                            if ((int)item == a)
                            {
                                hasValue = true;
                                break;
                            }
                        }
                        return hasValue == true;

                    }).WithMessage("Kaynak Tipi Seçimi Listede Yok");
                });

        }
    }
}
