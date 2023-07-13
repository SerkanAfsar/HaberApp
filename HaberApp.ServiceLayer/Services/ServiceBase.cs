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
    public class ServiceBase<D, T> : IServiceBase<D, T> where D : BaseEntity where T : BaseDto
    {
        private readonly IRepositoryBase<D> repositoryBase;
        private readonly ICacheProcess<T> cacheProcess;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ResponseResult<T> responseResult;

        public ServiceBase(IRepositoryBase<D> repositoryBase, ICacheProcess<T> cacheProcess, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.repositoryBase = repositoryBase;
            this.cacheProcess = cacheProcess;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.responseResult = new ResponseResult<T>();
        }

        public async Task<ResponseResult<T>> AddAsync(D entity, CancellationToken cancellationToken = default)
        {
            this.responseResult.Entity = this.mapper.Map<T>(await this.repositoryBase.CreateAsync(entity, cancellationToken));
            await this.unitOfWork.CommitAsync(cancellationToken);
            return this.responseResult;
        }

        public async Task<ResponseResult<T>> AddRangeAsync(IEnumerable<D> entities, CancellationToken cancellationToken = default)
        {
            var result = await this.repositoryBase.AddRangeAsync(entities, cancellationToken);
            await this.unitOfWork.CommitAsync(cancellationToken);
            this.responseResult.Entities = this.mapper.Map<List<T>>(await result.ToListAsync(cancellationToken));
            return this.responseResult;
        }

        public async Task<ResponseResult<T>> DeleteAsync(D entity, CancellationToken cancellationToken = default)
        {
            this.responseResult.Entity = this.mapper.Map<T>(await this.repositoryBase.DeleteAsync(entity, cancellationToken));
            await this.unitOfWork.CommitAsync(cancellationToken);
            return this.responseResult;
        }


        public async Task<ResponseResult<T>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var result = await this.repositoryBase.GetByIdAsync(id, cancellationToken) ?? null;
            if (result == null)
            {
                throw new NotFoundException($"{typeof(D).Name} Not Found");
            }
            this.responseResult.Entity = this.mapper.Map<T>(result);
            return this.responseResult;
        }

        public async Task<ResponseResult<T>> GetListAsync(Expression<Func<D, bool>> predicate = null, CancellationToken cancellationToken = default)
        {
            var list = await this.repositoryBase.GetListAsync(predicate);
            this.responseResult.Entities = this.mapper.Map<List<T>>(await list.ToListAsync());
            return this.responseResult;
        }

        public async Task<ResponseResult<T>> RemoveRangeAsync(IEnumerable<D> entities, CancellationToken cancellationToken = default)
        {
            var result = await this.repositoryBase.RemoveRangeAsync(entities, cancellationToken);
            await this.unitOfWork.CommitAsync(cancellationToken);
            this.responseResult.Entities = this.mapper.Map<List<T>>(await result.ToListAsync(cancellationToken));
            return this.responseResult;
        }

        public async Task<ResponseResult<T>> UpdateAsync(D entity, CancellationToken cancellationToken = default)
        {
            this.responseResult.Entity = this.mapper.Map<T>(await this.repositoryBase.UpdateAsync(entity, cancellationToken));
            return this.responseResult;
        }
    }
}
