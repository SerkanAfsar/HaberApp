using HaberApp.Core.DTOs.RequestDtos;
using HaberApp.WebService.CustomFilters;
using Microsoft.AspNetCore.Mvc;

namespace HaberApp.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CustomFilterAttribute<CategorySourceRequestDto>))]
    public class CategorySourcesController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddCategorySource([FromBody] CategorySourceRequestDto categorySource)
        {
            return Ok("Başarılı");
        }
    }
}
