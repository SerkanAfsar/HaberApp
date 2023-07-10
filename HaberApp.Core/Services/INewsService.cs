﻿using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;

namespace HaberApp.Core.Services
{
    public interface INewsService : IServiceBase<News, NewsResponseDto>
    {
    }
}
