using AutoMapper;
using HaberApp.Core.DTOs;
using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Models.Abstract;

namespace HaberApp.ServiceLayer.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryRequestDto, Category>().ReverseMap();
            CreateMap<Category, CategoryResponseDto>();

            CreateMap<CategorySourceRequestDto, CategorySource>();
            CreateMap<CategorySource, CategorySourceResponseDto>();

            CreateMap<NewsRequestDto, News>();
            CreateMap<News, NewsResponseDto>();

            CreateMap<BaseResponseDto, BaseEntity>().ReverseMap();

        }
    }
}
