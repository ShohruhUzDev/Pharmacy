using Microsoft.AspNetCore.Mvc;
using Pharmacy.Domain.Configurations;
using Pharmacy.Service.DTOs;
using Pharmacy.Service.Interfaces;

namespace Pharmacy.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {

        private readonly IBasketService basketService;

        public BasketController(IBasketService basketService)
        {
            this.basketService = basketService;
        }



        [HttpGet]
        public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
        {
            return Ok(await basketService.GetAllAsync(@params));
        }

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
            => Ok(await basketService.GetAsync(c => c.Id == id));

        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync(BasketForCreationDTO basketForCreationDTO)
        {
            var basketForCreation = new BasketForCreationDTO
            {
                UserId = basketForCreationDTO.UserId,
                TotalPrice = basketForCreationDTO.TotalPrice
            };

            return Ok(await basketService.CreateAsync(basketForCreation));
        }

        [HttpDelete("{Id}")]
        public async ValueTask<IActionResult> DeleteAsync(int Id) => 
            Ok(await basketService.DeleteAsync(c => c.Id == Id));

        [HttpPut("{id}")]
        public async ValueTask<IActionResult> UpdateAsync(
            [FromRoute] int id,
            [FromQuery] BasketForCreationDTO basketForCreationDTO
          )
        {

            return Ok(await basketService.UpdateAsync(id, basketForCreationDTO));
        }
    }
}
