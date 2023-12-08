using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Services;
using HaberApp.Core.Utils;
using HaberApp.ServiceLayer.Constants;
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

        [CustomAuthorize(Modules.NewsModule.Read)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNews(int id)
        {
            var result = HttpContext.Items["result"] as ResponseResult<NewsResponseDto>;
            return Ok(result);
        }
        [CustomAuthorize(Modules.NewsModule.Read)]
        [HttpGet]
        public async Task<IActionResult> GetNews(CancellationToken cancellationToken = default)
        {
            return Ok(await this.newsService.GetListAsync(null, cancellationToken));
        }

        [CustomAuthorize(Modules.NewsModule.Read)]
        [HttpGet("GetNewsByPagination/{pageIndex}/{limitSize}/{categoryId?}")]
        public async Task<IActionResult> GetAll(int pageIndex, int limitSize, int? categoryId = null, CancellationToken cancellationToken = default)
        {
            var result = await this.newsService.GetNewsByPaginationIncludeCategoryAsync(pageIndex, limitSize, categoryId, cancellationToken);
            return Ok(result);
        }
        [CustomAuthorize(Modules.NewsModule.Create)]
        [HttpPost]
        public async Task<IActionResult> AddNews([FromForm] NewsRequestDto newsRequestDto, CancellationToken cancellationToken = default)
        {
            return Ok(await this.newsService.AddAsync(newsRequestDto, cancellationToken));
        }
        [CustomAuthorize(Modules.NewsModule.Update)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNews(int id, [FromForm] NewsRequestDto newsRequestDto, CancellationToken cancellationToken = default)
        {
            return Ok(await this.newsService.UpdateAsync(id, newsRequestDto, cancellationToken));

        }

        [CustomAuthorize(Modules.NewsModule.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(int id, CancellationToken cancellationToken = default)
        {
            return Ok(await this.newsService.DeleteAsync(id, cancellationToken));
        }


        [CustomAuthorize(Modules.NewsModule.Update)]
        [HttpPost("SaveAllToDb")]
        public async Task<IActionResult> SaveAllToDb(CancellationToken cancellationToken = default)
        {
            await this.sourceService.SaveAllToDb(cancellationToken);
            return Ok(new
            {
                Success = true,

            });

        }
    }
}
