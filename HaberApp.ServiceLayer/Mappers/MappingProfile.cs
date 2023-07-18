using AutoMapper;
using HaberApp.Core.DTOs;
using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Models.Abstract;
using HaberApp.Core.Models.Enums;
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

            CreateMap<NewsRequestDto, News>().ForMember(a => a.NewsPicture, opt => opt.MapFrom(b => SaveImage(b.NewsPicture)));
            CreateMap<News, NewsResponseDto>();

            CreateMap<BaseResponseDto, BaseEntity>();

        }
        private string SaveImage(IFormFile file)
        {
            string imageExtension = Path.GetExtension(file.FileName);

            string imageName = Guid.NewGuid() + imageExtension;

            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{imageName}");

            using var stream = new FileStream(path, FileMode.Create);

            file.CopyTo(stream);
            return imageName;
        }

    }
}
