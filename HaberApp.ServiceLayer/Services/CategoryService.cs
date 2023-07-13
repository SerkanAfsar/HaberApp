using AutoMapper;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Repositories;
using HaberApp.Core.Services;
using HaberApp.Core.UnitOfWork;
using HaberApp.Core.Utils;
using HaberApp.ServiceLayer.Caching;
using Microsoft.EntityFrameworkCore;

namespace HaberApp.ServiceLayer.Services
{
    public class CategoryService : ServiceBase<Category, CategoryResponseDto>, ICategoryService
    {
        private readonly IRepositoryBase<Category> repositoryBase;
        private readonly ICacheProcess<CategoryResponseDto> cacheProcess;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ResponseResult<CategoryResponseDto> responseResult;
        public CategoryService(IRepositoryBase<Category> repositoryBase, ICacheProcess<CategoryResponseDto> cacheProcess, IUnitOfWork unitOfWork, IMapper mapper) : base(repositoryBase, cacheProcess, unitOfWork, mapper)
        {
            this.repositoryBase = repositoryBase;
            this.cacheProcess = cacheProcess;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.responseResult = new ResponseResult<CategoryResponseDto>();
        }

        public async Task<ResponseResult<CategoryResponseDto>> GetCachedCategoryListAsync()
        {
            if (!this.cacheProcess.DoesExist(CacheConstants.CategoryList))
            {
                var list = await this.repositoryBase.GetListAsync();
                this.cacheProcess.SetCachedDtos(CacheConstants.CategoryList, this.mapper.Map<List<CategoryResponseDto>>(await list.ToListAsync()));
            }
            this.responseResult.Entities = this.cacheProcess.GetCachedDtos(CacheConstants.CategoryList);
            return this.responseResult;
        }
    }
}
