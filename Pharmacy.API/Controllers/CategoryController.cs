using Microsoft.AspNetCore.Mvc;
using Pharmacy.Domain.Configurations;
using Pharmacy.Service.DTOs;
using Pharmacy.Service.Interfaces;

namespace Pharmacy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [HttpGet]
        public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
        {
            return Ok(await categoryService.GetAllAsync(@params));
        }

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
            => Ok(await categoryService.GetAsync(c => c.Id == id));

        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync(string cantent)
        {
            var categoryForCreationDTO = new CategoryForCreationDTO
            {
                Content = cantent
            };

            return Ok(await categoryService.CreateAsync(categoryForCreationDTO));
        }

        [HttpDelete("{Id}")]
        public async ValueTask<IActionResult> DeleteAsync( int Id)
            => Ok(await categoryService.DeleteAsync(c => c.Id == Id));

        [HttpPut("{id}")]
        public async ValueTask<IActionResult> UpdateAsync(
            [FromRoute] int id,
            [FromQuery] CategoryForCreationDTO categoryForCreationDTO
           )
        {

            return Ok(await categoryService.UpdateAsync(id, categoryForCreationDTO));
        }

    }
}
