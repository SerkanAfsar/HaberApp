using AutoMapper;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Repositories;
using HaberApp.Core.Services;
using HaberApp.Core.UnitOfWork;
using HaberApp.ServiceLayer.Caching;

namespace HaberApp.ServiceLayer.Services
{
    public class CategorySourceService : ServiceBase<CategorySource, CategorySourceResponseDto>, ICategorySourceService
    {
        public CategorySourceService(IRepositoryBase<CategorySource> repositoryBase, ICacheProcess<CategorySourceResponseDto> cacheProcess, IUnitOfWork unitOfWork, IMapper mapper) : base(repositoryBase, cacheProcess, unitOfWork, mapper)
        {
        }
    }
}
