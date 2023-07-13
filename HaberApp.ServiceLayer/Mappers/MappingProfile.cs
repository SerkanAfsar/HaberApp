using AutoMapper;
using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;

namespace HaberApp.ServiceLayer.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryRequestDto, Category>();
            CreateMap<Category, CategoryResponseDto>();

            CreateMap<CategorySourceRequestDto, CategorySource>();
            CreateMap<CategorySource, CategorySourceResponseDto>();

            CreateMap<NewsRequestDto, News>();
            CreateMap<News, NewsResponseDto>();

        }
    }
}
