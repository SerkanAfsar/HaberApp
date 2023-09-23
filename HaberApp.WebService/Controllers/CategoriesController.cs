using AutoMapper;
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
    [ServiceFilter(typeof(CustomFilterAttribute<Category, CategoryRequestDto, CategoryResponseDto>))]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        private readonly ResponseResult<CategoryResponseDto> responseResult;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
            this.responseResult = new ResponseResult<CategoryResponseDto>();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = HttpContext.Items["result"] as ResponseResult<CategoryResponseDto>;
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await this.categoryService.GetListAsync(null, cancellationToken);
            return Ok(result);
        }
        [HttpGet("GetCategoriesByPagination/{pageIndex}/{limit}")]
        public async Task<IActionResult> GetAll(int pageIndex, int limit, CancellationToken cancellationToken = default)
        {
            var result = await this.categoryService.GetCategoryListByPaginationAsync(pageIndex, limit, cancellationToken);
            return Ok(result);
        }
        [HttpGet("GetMenuList")]
        public async Task<IActionResult> GetMenuList(CancellationToken cancellationToken = default)
        {
            return Ok(await this.categoryService.GetCategoryListOrderByQueueAsync(cancellationToken));
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryRequestDto model, CancellationToken cancellationToken = default)
        {
            var result = await this.categoryService.AddAsync(model, cancellationToken);
            await this.categoryService.SetCategoryMenuListByQueueCacheAsync(cancellationToken);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequestDto model, CancellationToken cancellationToken = default)
        {
            var result = await this.categoryService.UpdateAsync(id, model, cancellationToken);
            await this.categoryService.SetCategoryMenuListByQueueCacheAsync(cancellationToken);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken = default)
        {
            var result = await this.categoryService.DeleteAsync(id, cancellationToken);
            await this.categoryService.SetCategoryMenuListByQueueCacheAsync(cancellationToken);
            return Ok(result);
        }
        [HttpPost("deneme")]
        public async Task<IActionResult> Test(CancellationToken cancellationToken = default)
        {
            return Ok(await this.categoryService.TestCategoryResponse(cancellationToken));
        }
    }
}
