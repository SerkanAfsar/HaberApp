using FluentValidation;
using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.Repositories;

namespace HaberApp.ServiceLayer.Validations
{
    public class NewsRequestValidator : AbstractValidator<NewsRequestDto>
    {
        private readonly INewsRepository newsRepository;
        private readonly ICategoryRepository categoryRepository;
        public NewsRequestValidator(INewsRepository newsRepository, ICategoryRepository categoryRepository)
        {
            this.newsRepository = newsRepository;
            this.categoryRepository = categoryRepository;

            RuleFor(a => a.NewsTitle)
                .NotEmpty().WithMessage("Haber Başlık Boş Bırakılamaz")
                .NotNull().WithMessage("Haber Başlık Boş Bırakılamaz").DependentRules(() =>
                {
                    RuleFor(a => a.NewsTitle).MustAsync(async (a, CancellationToken) =>
                    {
                        var result = await this.newsRepository.GetByFilterAsync(a => a.NewsTitle.ToLower().Trim() == a.ToString().ToLower().Trim()) ?? null;
                        return result != null;
                    }).WithMessage("Girdiğiniz Başlıkta Haber Mevcut");
                });


            RuleFor(a => a.SeoTitle)
                .NotEmpty().WithMessage("Seo Title Boş Bırakılamaz")
                .NotNull().WithMessage("Seo Title Boş Bırakılamaz");

            RuleFor(a => a.SeoDesctiption)
                .NotEmpty().WithMessage("Seo Description Boş Bırakılamaz")
                .NotNull().WithMessage("Seo Description Boş Bırakılamaz");

            RuleFor(a => a.NewsSubTitle)
               .NotEmpty().WithMessage("Alt Başlık Boş Bırakılamaz")
               .NotNull().WithMessage("Alt Başlık Boş Bırakılamaz");

            RuleFor(a => a.NewsContent)
               .NotEmpty().WithMessage("Haber İçerik Boş Bırakılamaz")
               .NotNull().WithMessage("Haber İçerik Boş Bırakılamaz");

            RuleFor(a => a.CategoryId)
               .NotEmpty().WithMessage("Kategori Seçiniz")
               .NotNull().WithMessage("Kategori Seçiniz").DependentRules(() =>
               {
                   RuleFor(a => a.CategoryId).MustAsync(async (a, CancellationToken) =>
                   {
                       var result = await this.categoryRepository.GetByIdAsync(a, CancellationToken) ?? null;
                       return result != null;
                   }).WithMessage("Kategori Seçimi Yanlış..");
               });

        }

    }
}
