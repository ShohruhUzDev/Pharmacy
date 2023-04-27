using Microsoft.AspNetCore.Mvc;
using Pharmacy.Domain.Configurations;
using Pharmacy.Service.DTOs;
using Pharmacy.Service.Interfaces;

namespace Pharmacy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicineOrderController : ControllerBase
    {
        private readonly IMedicineOrderService medicineOrderService;
        public MedicineOrderController(IMedicineOrderService medicineOrderService)
        {
            this.medicineOrderService = medicineOrderService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
        {
            return Ok(await medicineOrderService.GetAllAsync(@params));
        }

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
            => Ok(await medicineOrderService.GetAsync(c => c.Id == id));

        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync(MedicineOrderForCreationDTO medicineOrderForCreationDTO)
        {
            var medicineOrderForCreation = new MedicineOrderForCreationDTO
            {
                Count = medicineOrderForCreationDTO.Count,
                BasketId = medicineOrderForCreationDTO.BasketId,
                MedicineId = medicineOrderForCreationDTO.MedicineId

            };

            return Ok(await medicineOrderService.CreateAsync(medicineOrderForCreation));
        }

        [HttpDelete("{Id}")]
        public async ValueTask<IActionResult> DeleteAsync([FromRoute] int id)
            => Ok(await medicineOrderService.DeleteAsync(c => c.Id == id));

        [HttpPut("{id}")]
        public async ValueTask<IActionResult> UpdateAsync(
            [FromRoute] int id,
            [FromQuery] MedicineOrderForCreationDTO medicineOrderForCreationDTO
          )
        {

            return Ok(await medicineOrderService.UpdateAsync(id, medicineOrderForCreationDTO));
        }
    }
}
