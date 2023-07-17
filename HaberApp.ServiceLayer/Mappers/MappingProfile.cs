using AutoMapper;
using HaberApp.Core.DTOs;
using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Models.Abstract;
using HaberApp.ServiceLayer.Utils;

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

            CreateMap<CategorySource, CategorySourceResponseDto>().ForMember(a => a.CategoryStringName, opt =>
            {
                opt.MapFrom(a => GetCategoryName(a.CategoryId).Result);
            });

            CreateMap<NewsRequestDto, News>();
            CreateMap<News, NewsResponseDto>();

            CreateMap<BaseResponseDto, BaseEntity>();

        }
        private async Task<string> GetCategoryName(int categoryId)
        {
            //var result = await this.categoryService.GetByIdAsync(categoryId);
            //return result.Entity!.CategoryName;
            return "deneme";
        }
    }
}
