using HaberApp.Core.DTOs;
using HaberApp.Core.Models.Abstract;
using HaberApp.Core.Utils;
using System.Linq.Expressions;

namespace HaberApp.Core.Services
{
    public interface IServiceBase<Domain, RequestDto, ResponseDto> where Domain : BaseEntity
        where RequestDto : BaseRequestDto
        where ResponseDto : BaseResponseDto
    {
        Task<ResponseResult<ResponseDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ResponseResult<ResponseDto>> GetListAsync(Expression<Func<Domain, bool>> predicate = null, CancellationToken cancellationToken = default);
        Task<ResponseResult<ResponseDto>> AddAsync(RequestDto Dto, CancellationToken cancellationToken = default);
        Task<ResponseResult<ResponseDto>> UpdateAsync(RequestDto Dto, CancellationToken cancellationToken = default);
        Task<ResponseResult<ResponseDto>> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<ResponseResult<ResponseDto>> AddRangeAsync(IEnumerable<RequestDto> Dtos, CancellationToken cancellationToken = default);
        Task<ResponseResult<ResponseDto>> RemoveRangeAsync(IEnumerable<RequestDto> Dtos, CancellationToken cancellationToken = default);
    }
}
