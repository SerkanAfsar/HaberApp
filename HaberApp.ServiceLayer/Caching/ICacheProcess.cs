﻿using HaberApp.Core.DTOs;

namespace HaberApp.ServiceLayer.Caching
{
    public interface ICacheProcess<T> where T : BaseDto
    {
        void SetCachedDtos(string cacheKey, IEnumerable<T> entities);
        List<T>? GetCachedDtos(string cacheKey);
        bool DoesExist(string cacheKey);
    }
}
