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

            CreateMap<NewsRequestDto, News>()
                .ForMember(a => a.NewsPicture, opt => opt.MapFrom(b => SaveImage(b.NewsPicture)));
            CreateMap<News, NewsResponseDto>().ForMember(a => a.SourceUrl, opt => opt.MapFrom(b => (int)b.NewsSource));

            CreateMap<BaseRequestDto, BaseEntity>();
            CreateMap<BaseEntity, BaseResponseDto>();

            CreateMap<AppRole, RoleResponseDto>().ForMember(a => a.RoleName, opt => opt.MapFrom(b => b.Name))
                .ForMember(a => a.RoleId, opt => opt.MapFrom(b => b.Id))
                .ForMember(a => a.Id, opt => opt.Ignore());

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
