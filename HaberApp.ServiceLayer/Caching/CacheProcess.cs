using HaberApp.Core.DTOs;
using Microsoft.Extensions.Caching.Memory;

namespace HaberApp.ServiceLayer.Caching
{
    public class CacheProcess<T> : ICacheProcess<T> where T : BaseDto
    {
        private readonly IMemoryCache memoryCache;
        public CacheProcess(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        public bool DoesExist(string cacheKey)
        {
            return this.memoryCache.TryGetValue(cacheKey, out _);
        }

        public List<T>? GetCachedDtos(string cacheKey)
        {
            return this.memoryCache.Get<List<T>>(cacheKey) ?? null;
        }

        public void SetCachedDtos(string cacheKey, IEnumerable<T> entities)
        {
            this.memoryCache.Set(cacheKey, entities);
        }
    }
}
