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
        private readonly ISourceService sourceService;
        public NewsController(INewsService newsService, ISourceService sourceService)
        {
            this.newsService = newsService;
            this.sourceService = sourceService;
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

        [HttpGet("GetNewsByPagination/{pageIndex}/{limitSize}/{categoryId?}")]
        public async Task<IActionResult> GetAll(int pageIndex, int limitSize, int? categoryId = null, CancellationToken cancellationToken = default)
        {
            var result = await this.newsService.GetNewsByPaginationIncludeCategoryAsync(pageIndex, limitSize, categoryId, cancellationToken);
            return Ok(result);
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
        [HttpPost("SaveAllToDb")]
        public async Task<IActionResult> SaveAllToDb(CancellationToken cancellationToken = default)
        {
            await this.sourceService.SaveAllToDb();
            return Ok("Test Deneme");
        }
    }
}
