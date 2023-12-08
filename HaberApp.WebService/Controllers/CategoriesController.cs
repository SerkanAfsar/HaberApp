using AutoMapper;
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
        [CustomAuthorize(Modules.CategoryModule.Read)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = HttpContext.Items["result"] as ResponseResult<CategoryResponseDto>;
            return Ok(result);
        }
        [CustomAuthorize(Modules.CategoryModule.Read)]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await this.categoryService.GetListAsync(null, cancellationToken);
            return Ok(result);
        }
        [CustomAuthorize(Modules.CategoryModule.Read)]
        [HttpGet("GetCategoriesByPagination/{pageIndex}/{limit}")]
        public async Task<IActionResult> GetAll(int pageIndex, int limit, CancellationToken cancellationToken = default)
        {
            var result = await this.categoryService.GetCategoryListOrderByPaginationAsync(pageIndex, limit, cancellationToken);
            return Ok(result);
        }

        [CustomAuthorize(Modules.CategoryModule.Read)]
        [HttpGet("GetMenuList")]
        public async Task<IActionResult> GetMenuList(CancellationToken cancellationToken = default)
        {
            return Ok(await this.categoryService.GetCategoryListOrderByQueueAsync(cancellationToken));
        }
        [CustomAuthorize(Modules.CategoryModule.Create)]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryRequestDto model, CancellationToken cancellationToken = default)
        {
            var result = await this.categoryService.AddAsync(model, cancellationToken);

            return Ok(result);
        }
        [CustomAuthorize(Modules.CategoryModule.Update)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequestDto model, CancellationToken cancellationToken = default)
        {
            var result = await this.categoryService.UpdateAsync(id, model, cancellationToken);
            return Ok(result);
        }
        [CustomAuthorize(Modules.CategoryModule.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken = default)
        {
            var result = await this.categoryService.DeleteAsync(id, cancellationToken);
            return Ok(result);
        }
        [CustomAuthorize(Modules.CategoryModule.Update)]
        [HttpGet("UpCategory/{id}")]
        public async Task<IActionResult> UpCategory(int id, CancellationToken cancellationToken = default)
        {
            var category = HttpContext.Items["result"] as CategoryResponseDto;
            return Ok(await this.categoryService.UpCategoryAsync(category, cancellationToken));
        }

        [CustomAuthorize(Modules.CategoryModule.Update)]
        [HttpGet("DownCategory/{id}")]
        public async Task<IActionResult> DownCategory(int id, CancellationToken cancellationToken = default)
        {
            var category = HttpContext.Items["result"] as CategoryResponseDto;
            return Ok(await this.categoryService.DownCategoryAsync(category, cancellationToken));
        }


    }
}
