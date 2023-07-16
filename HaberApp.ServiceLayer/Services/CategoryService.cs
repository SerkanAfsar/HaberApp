using AutoMapper;
using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Repositories;
using HaberApp.Core.Services;
using HaberApp.Core.UnitOfWork;
using HaberApp.ServiceLayer.Caching;

namespace HaberApp.ServiceLayer.Services
{
    public class CategoryService : ServiceBase<Category, CategoryRequestDto, CategoryResponseDto>, ICategoryService
    {
        public CategoryService(IRepositoryBase<Category> repositoryBase, ICacheProcess<CategoryResponseDto> cacheProcess, IUnitOfWork unitOfWork, IMapper mapper) : base(repositoryBase, cacheProcess, unitOfWork, mapper)
        {
        }
    }
}
