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
    [ServiceFilter(typeof(CustomFilterAttribute<CategorySource, CategorySourceRequestDto, CategorySourceResponseDto>))]
    public class CategorySourcesController : ControllerBase
    {

        private readonly ICategorySourceService categorySourceService;
        private readonly ResponseResult<CategorySourceResponseDto> result;
        public CategorySourcesController(ICategorySourceService categorySourceService)
        {
            this.categorySourceService = categorySourceService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = HttpContext.Items["result"] as ResponseResult<CategorySourceResponseDto>;
            return Ok(result);

        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await this.categorySourceService.GetListAsync(null, cancellationToken);
            return Ok(result);
        }
        [HttpGet("GetCategorySourcesByPagination/{pageIndex}/{limitSize}/{categoryId?}")]
        public async Task<IActionResult> GetAll(int pageIndex, int limitSize, int? categoryId = null, CancellationToken cancellationToken = default)
        {
            var result = await this.categorySourceService.GetCategorySourcesByPagination(pageIndex, limitSize, categoryId, cancellationToken);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategorySource([FromBody] CategorySourceRequestDto model, CancellationToken cancellationToken = default)
        {
            var result = await this.categorySourceService.AddAsync(model, cancellationToken);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategorySource(int id, [FromBody] CategorySourceRequestDto model, CancellationToken cancellationToken = default)
        {
            var result = await this.categorySourceService.UpdateAsync(id, model, cancellationToken);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategorySource(int id, CancellationToken cancellationToken = default)
        {
            var result = await this.categorySourceService.DeleteAsync(id, cancellationToken);
            return Ok(result);
        }
    }
}
