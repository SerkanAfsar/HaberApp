using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Utils;

namespace HaberApp.Core.Services
{
    public interface ICategoryService : IServiceBase<Category, CategoryRequestDto, CategoryResponseDto>
    {
        Task<ResponseResult<CategoryResponseDto>> GetCategoryListOrderByQueueAsync(CancellationToken cancellationToken = default);
        Task<ResponseResult<CategoryResponseDto>> GetCategoryListOrderByPaginationAsync(int startIndex = 1, int count = 10, CancellationToken cancellationToken = default);
        Task<ResponseResult<CategoryResponseDto>> UpCategoryAsync(CategoryResponseDto category, CancellationToken cancellationToken = default);
        Task<ResponseResult<CategoryResponseDto>> DownCategoryAsync(CategoryResponseDto category, CancellationToken cancellationToken = default);
    }
}
