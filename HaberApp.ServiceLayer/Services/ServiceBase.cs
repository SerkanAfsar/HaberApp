using AutoMapper;
using HaberApp.Core.DTOs;
using HaberApp.Core.Models.Abstract;
using HaberApp.Core.Repositories;
using HaberApp.Core.Services;
using HaberApp.Core.UnitOfWork;
using HaberApp.Core.Utils;
using HaberApp.ServiceLayer.Caching;
using HaberApp.ServiceLayer.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HaberApp.ServiceLayer.Services
{
    public class ServiceBase<Domain, RequestDto, ResponseDto> : IServiceBase<Domain, RequestDto, ResponseDto> where Domain : BaseEntity where RequestDto : BaseDto
        where ResponseDto : BaseDto
    {
        private readonly IRepositoryBase<Domain> repositoryBase;
        private readonly ICacheProcess<ResponseDto> cacheProcess;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ResponseResult<ResponseDto> responseResult;

        public ServiceBase(IRepositoryBase<Domain> repositoryBase, ICacheProcess<ResponseDto> cacheProcess, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.repositoryBase = repositoryBase;
            this.cacheProcess = cacheProcess;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.responseResult = new ResponseResult<ResponseDto>();
        }

        public async Task<ResponseResult<ResponseDto>> AddAsync(RequestDto Dto, CancellationToken cancellationToken = default)
        {
            var entity = await this.repositoryBase.CreateAsync(this.mapper.Map<Domain>(Dto), cancellationToken);
            this.responseResult.Entity = this.mapper.Map<ResponseDto>(entity);
            await this.unitOfWork.CommitAsync(cancellationToken);
            return this.responseResult;
        }

        public async Task<ResponseResult<ResponseDto>> AddRangeAsync(IEnumerable<RequestDto> Dtos, CancellationToken cancellationToken = default)
        {
            var result = await this.repositoryBase.AddRangeAsync(this.mapper.Map<List<Domain>>(Dtos), cancellationToken);
            await this.unitOfWork.CommitAsync(cancellationToken);
            this.responseResult.Entities = this.mapper.Map<List<ResponseDto>>(await result.ToListAsync(cancellationToken));
            return this.responseResult;
        }

        public async Task<ResponseResult<ResponseDto>> DeleteAsync(RequestDto Dto, CancellationToken cancellationToken = default)
        {
            this.responseResult.Entity = this.mapper.Map<ResponseDto>(await this.repositoryBase.DeleteAsync(this.mapper.Map<Domain>(Dto), cancellationToken));
            await this.unitOfWork.CommitAsync(cancellationToken);
            return this.responseResult;
        }

        public async Task<ResponseResult<ResponseDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var result = await this.repositoryBase.GetByIdAsync(id, cancellationToken) ?? null;
            if (result == null)
            {
                throw new NotFoundException($"{typeof(Domain).Name} with Id {id} Not Found");
            }
            this.responseResult.Entity = this.mapper.Map<ResponseDto>(result);
            return this.responseResult;
        }

        public async Task<ResponseResult<ResponseDto>> GetListAsync(Expression<Func<Domain, bool>> predicate = null, CancellationToken cancellationToken = default)
        {
            var list = await this.repositoryBase.GetListAsync(predicate);
            this.responseResult.Entities = this.mapper.Map<List<ResponseDto>>(list);
            return this.responseResult;
        }

        public async Task<ResponseResult<ResponseDto>> RemoveRangeAsync(IEnumerable<RequestDto> Dtos, CancellationToken cancellationToken = default)
        {
            var result = await this.repositoryBase.RemoveRangeAsync(this.mapper.Map<List<Domain>>(Dtos), cancellationToken);
            await this.unitOfWork.CommitAsync(cancellationToken);
            this.responseResult.Entities = this.mapper.Map<List<ResponseDto>>(await result.ToListAsync(cancellationToken));
            return this.responseResult;
        }

        public async Task<ResponseResult<ResponseDto>> UpdateAsync(RequestDto Dto, CancellationToken cancellationToken = default)
        {
            this.responseResult.Entity = this.mapper.Map<ResponseDto>(await this.repositoryBase.UpdateAsync(this.mapper.Map<Domain>(Dto), cancellationToken));
            return this.responseResult;
        }





        //public async Task<ResponseResult<T>> DeleteAsync(T Dto, CancellationToken cancellationToken = default)
        //{
        //    this.responseResult.Entity = this.mapper.Map<T>(await this.repositoryBase.DeleteAsync(this.mapper.Map<D>(Dto), cancellationToken));
        //    await this.unitOfWork.CommitAsync(cancellationToken);
        //    return this.responseResult;
        //}


        //public async Task<ResponseResult<T>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        //{
        //    var result = await this.repositoryBase.GetByIdAsync(id, cancellationToken) ?? null;
        //    if (result == null)
        //    {
        //        throw new NotFoundException($"{typeof(D).Name} Not Found");
        //    }
        //    this.responseResult.Entity = this.mapper.Map<T>(result);
        //    return this.responseResult;
        //}

        //public async Task<ResponseResult<T>> GetListAsync(Expression<Func<D, bool>> predicate = null, CancellationToken cancellationToken = default)
        //{
        //    var list = await this.repositoryBase.GetListAsync(predicate);
        //    this.responseResult.Entities = this.mapper.Map<List<T>>(list);
        //    return this.responseResult;
        //}

        //public async Task<ResponseResult<T>> RemoveRangeAsync(IEnumerable<T> Dtos, CancellationToken cancellationToken = default)
        //{
        //    var result = await this.repositoryBase.RemoveRangeAsync(this.mapper.Map<List<D>>(Dtos), cancellationToken);
        //    await this.unitOfWork.CommitAsync(cancellationToken);
        //    this.responseResult.Entities = this.mapper.Map<List<T>>(await result.ToListAsync(cancellationToken));
        //    return this.responseResult;
        //}

        //public async Task<ResponseResult<T>> UpdateAsync(T Dto, CancellationToken cancellationToken = default)
        //{
        //    this.responseResult.Entity = this.mapper.Map<T>(await this.repositoryBase.UpdateAsync(this.mapper.Map<D>(Dto), cancellationToken));
        //    return this.responseResult;
        //}
    }
}
