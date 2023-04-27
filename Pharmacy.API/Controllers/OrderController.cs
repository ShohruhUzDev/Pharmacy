using Microsoft.AspNetCore.Mvc;
using Pharmacy.Domain.Configurations;
using Pharmacy.Service.DTOs;
using Pharmacy.Service.Interfaces;

namespace Pharmacy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }



        [HttpGet]
        public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
        {
            return Ok(await orderService.GetAllAsync(@params));
        }

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
            => Ok(await orderService.GetAsync(c => c.Id == id));

        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync(OrdersForCreationDTO ordersForCreationDTO)
        {
            var orderForCreation = new OrdersForCreationDTO
            {
                Location = ordersForCreationDTO.Location,
                BasketId = ordersForCreationDTO.BasketId,
                CustomerId = ordersForCreationDTO.CustomerId,
                IsPayed = ordersForCreationDTO.IsPayed
            };

            return Ok(await orderService.CreateAsync(orderForCreation));
        }

        [HttpDelete("{Id}")]
        public async ValueTask<IActionResult> DeleteAsync([FromRoute] int id)
            => Ok(await orderService.DeleteAsync(c => c.Id == id));

        [HttpPut("{id}")]
        public async ValueTask<IActionResult> UpdateAsync(
            [FromRoute] int id,
            [FromQuery] OrdersForCreationDTO orderForCreationDTO
          )
        {

            return Ok(await orderService.UpdateAsync(id, orderForCreationDTO));
        }
    }
}
