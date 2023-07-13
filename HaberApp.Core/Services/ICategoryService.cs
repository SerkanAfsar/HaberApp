using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Utils;

namespace HaberApp.Core.Services
{
    public interface ICategoryService : IServiceBase<Category, CategoryResponseDto>
    {
        Task<ResponseResult<CategoryResponseDto>> GetCachedCategoryListAsync();
    }
}
