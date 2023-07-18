using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Services;
using HaberApp.Core.Utils;
using HaberApp.WebService.CustomFilters;
using Microsoft.AspNetCore.Mvc;

namespace HaberApp.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CustomFilterAttribute<News, NewsRequestDto, NewsResponseDto>))]
    public class NewsController : ControllerBase
    {
        private readonly INewsService newsService;
        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNews(int id)
        {
            var result = HttpContext.Items["result"] as ResponseResult<NewsResponseDto>;
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetNews(CancellationToken cancellationToken = default)
        {
            return Ok(await this.newsService.GetListAsync(null, cancellationToken));
        }
        [HttpPost]
        public async Task<IActionResult> AddNews([FromForm] NewsRequestDto newsRequestDto, CancellationToken cancellationToken = default)
        {
            return Ok(await this.newsService.AddAsync(newsRequestDto, cancellationToken));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNews(int id, [FromForm] NewsRequestDto newsRequestDto, CancellationToken cancellationToken = default)
        {
            return Ok(await this.newsService.UpdateAsync(id, newsRequestDto, cancellationToken));

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(int id, CancellationToken cancellationToken = default)
        {
            return Ok(await this.newsService.DeleteAsync(id, cancellationToken));
        }
    }
}
