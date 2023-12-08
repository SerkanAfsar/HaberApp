using AutoMapper;
using HaberApp.Core.DTOs;
using HaberApp.Core.Models.Abstract;
using HaberApp.Core.Repositories;
using HaberApp.Core.Services;
using HaberApp.Core.UnitOfWork;
using HaberApp.Core.Utils;
using HaberApp.ServiceLayer.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HaberApp.ServiceLayer.Services
{
    public class ServiceBase<Domain, RequestDto, ResponseDto> : IServiceBase<Domain, RequestDto, ResponseDto> where Domain : BaseEntity where RequestDto : BaseRequestDto
        where ResponseDto : BaseResponseDto
    {
        private readonly IRepositoryBase<Domain> repositoryBase;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ResponseResult<ResponseDto> responseResult;

        public ServiceBase(IRepositoryBase<Domain> repositoryBase, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.repositoryBase = repositoryBase;

            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.responseResult = new ResponseResult<ResponseDto>();
        }

        public virtual async Task<ResponseResult<ResponseDto>> AddAsync(RequestDto Dto, CancellationToken cancellationToken = default)
        {
            var entity = await this.repositoryBase.CreateAsync(this.mapper.Map<Domain>(Dto), cancellationToken);
            await this.unitOfWork.CommitAsync(cancellationToken);
            this.responseResult.Entity = this.mapper.Map<ResponseDto>(entity);
            return this.responseResult;
        }

        public virtual async Task<ResponseResult<ResponseDto>> AddRangeAsync(IEnumerable<RequestDto> Dtos, CancellationToken cancellationToken = default)
        {
            var result = await this.repositoryBase.AddRangeAsync(this.mapper.Map<List<Domain>>(Dtos), cancellationToken);
            await this.unitOfWork.CommitAsync(cancellationToken);
            this.responseResult.Entities = this.mapper.Map<List<ResponseDto>>(await result.ToListAsync(cancellationToken));
            return this.responseResult;
        }

        public virtual async Task<ResponseResult<ResponseDto>> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            this.responseResult.Entity = this.mapper.Map<ResponseDto>(await this.repositoryBase.DeleteAsync(id, cancellationToken));
            await this.unitOfWork.CommitAsync(cancellationToken);
            return this.responseResult;
        }

        public virtual async Task<ResponseResult<ResponseDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var result = await this.repositoryBase.GetByIdAsync(id, cancellationToken) ?? null;
            if (result == null)
            {
                throw new NotFoundException($"{typeof(Domain).Name} with Id {id} Not Found");
            }
            this.responseResult.Entity = this.mapper.Map<ResponseDto>(result);
            return this.responseResult;
        }

        public virtual async Task<ResponseResult<ResponseDto>> GetListAsync(Expression<Func<Domain, bool>> predicate = null, CancellationToken cancellationToken = default)
        {
            var list = await this.repositoryBase.GetListAsync(predicate);
            this.responseResult.Entities = this.mapper.Map<List<ResponseDto>>(list);
            return this.responseResult;
        }

        public virtual async Task<ResponseResult<ResponseDto>> RemoveRangeAsync(IEnumerable<RequestDto> Dtos, CancellationToken cancellationToken = default)
        {
            var result = await this.repositoryBase.RemoveRangeAsync(this.mapper.Map<List<Domain>>(Dtos), cancellationToken);
            await this.unitOfWork.CommitAsync(cancellationToken);
            this.responseResult.Entities = this.mapper.Map<List<ResponseDto>>(await result.ToListAsync(cancellationToken));
            return this.responseResult;
        }

        public virtual async Task<ResponseResult<ResponseDto>> UpdateAsync(int id, RequestDto Dto, CancellationToken cancellationToken = default)
        {
            var entity = await this.repositoryBase.UpdateAsync(id, this.mapper.Map<Domain>(Dto), cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException($"{typeof(Domain).Name} with Id {id} Not Found");
            }
            this.responseResult.Entity = this.mapper.Map<ResponseDto>(entity);
            await this.unitOfWork.CommitAsync();
            return this.responseResult;
        }

    }
}
