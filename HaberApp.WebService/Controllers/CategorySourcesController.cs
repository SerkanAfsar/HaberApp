using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.Core.DTOs.ResponseDtos;
using HaberApp.Core.Models;
using HaberApp.Core.Utils;
using HaberApp.WebService.CustomFilters;
using Microsoft.AspNetCore.Mvc;

namespace HaberApp.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CustomFilterAttribute<Category, CategoryRequestDto, CategoryResponseDto>))]
    public class CategorySourcesController : ControllerBase
    {

        private readonly ResponseResult<CategorySourceResponseDto> result;
        public CategorySourcesController()
        {

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = HttpContext.Items["result"] as ResponseResult<CategorySourceResponseDto>;
            return Ok(result);

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategorySource([FromBody] CategoryRequestDto model, CancellationToken cancellationToken = default)
        {

            return Ok();
        }
    }
}
