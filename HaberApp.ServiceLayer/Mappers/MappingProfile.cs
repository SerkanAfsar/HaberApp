using AutoMapper;
using HaberApp.Core.DTOs;
using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Models.Abstract;
using HaberApp.Core.Models.Enums;
using HaberApp.Core.Repositories;
using HaberApp.ServiceLayer.Utils;
using Microsoft.AspNetCore.Http;

namespace HaberApp.ServiceLayer.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<CategoryRequestDto, Category>()
                .ForMember(a => a.SeoUrl, b => b.MapFrom(c => StringHelper.KarakterDuzelt(c.CategoryName)));

            CreateMap<Category, CategoryResponseDto>();

            CreateMap<CategorySourceRequestDto, CategorySource>();
            CreateMap<CategorySource, CategorySourceResponseDto>()
                .ForMember(a => a.SourceTypeName, b => b.MapFrom(c => Enum.GetName(typeof(NewsSource), c.SourceType)));

            CreateMap<NewsRequestDto, News>()
                .ForMember(a => a.NewsPicture, opt => opt.MapFrom<CustomValueResolver>())
                .ForMember(a => a.NewsSource, opt => opt.MapFrom(c => (NewsSource)c.NewsSource))
                .ForMember(a => a.SeoUrl, opt => opt.MapFrom(c => StringHelper.KarakterDuzelt(c.NewsTitle)));


            CreateMap<News, NewsResponseDto>()
                .ForMember(a => a.SourceUrl, opt => opt.MapFrom(b => (int)b.NewsSource))
                .ForMember(a => a.NewsPicture, opt => opt.MapFrom(a => a.NewsPicture ?? "no-image"));

            CreateMap<BaseRequestDto, BaseEntity>();
            CreateMap<BaseEntity, BaseResponseDto>();

            CreateMap<AppRole, RoleResponseDto>().ForMember(a => a.RoleName, opt => opt.MapFrom(b => b.Name))
                .ForMember(a => a.RoleId, opt => opt.MapFrom(b => b.Id))
                .ForMember(a => a.Id, opt => opt.Ignore());

        }


    }
    public class CustomValueResolver : IValueResolver<NewsRequestDto, News, string>
    {
        private readonly INewsRepository newsRepository;
        public CustomValueResolver(INewsRepository newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public string Resolve(NewsRequestDto source, News destination, string destMember, ResolutionContext context)
        {
            var entity = this.newsRepository.GetByFilterAsync(a => a.NewsTitle == source.NewsTitle).Result;
            if (entity != null)
            {
                if (source.NewsPicture != null)
                {
                    return SaveImage(source.NewsTitle, source.NewsPicture);
                }
                return entity.NewsPicture;
            }
            return SaveImage(source.NewsTitle, source.NewsPicture);


        }
        private string SaveImage(string title, IFormFile file)
        {

            if (file != null)
            {
                string imageExtension = Path.GetExtension(file.FileName);

                string imageName = StringHelper.KarakterDuzelt(title) + imageExtension;

                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{imageName}");

                using var stream = new FileStream(path, FileMode.Create);

                file.CopyTo(stream);
                return imageName;
            }
            return string.Empty;


        }
    }
}
