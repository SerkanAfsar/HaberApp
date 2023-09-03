using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Utils;

namespace HaberApp.Core.Services
{
    public interface ICategoryService : IServiceBase<Category, CategoryRequestDto, CategoryResponseDto>
    {
        Task<ResponseResult<CategoryResponseDto>> GetCategoryListOrderByQueueAsync(CancellationToken cancellationToken = default);
        Task SetCategoryMenuListByQueueCacheAsync(CancellationToken cancellationToken = default);
        Task<ResponseResult<CategoryResponseDto>> GetCategoryListByPaginationAsync(int pageIndex = 1, int takeCount = 10, CancellationToken cancellationToken = default);
        Task<ResponseResult<CategoryResponseDto>> TestCategoryResponse(CancellationToken cancellationToken = default);
    }
}
