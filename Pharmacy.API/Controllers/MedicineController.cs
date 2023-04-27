using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Api.Helpers;
using Pharmacy.Domain.Configurations;
using Pharmacy.Service.DTOs;
using Pharmacy.Service.Interfaces;

namespace Pharmacy.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService medicineService;
        public MedicineController(IMedicineService medicineService)
        {
            this.medicineService = medicineService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
        {
            return Ok(await medicineService.GetAllAsync(@params));
        }

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
            => Ok(await medicineService.GetAsync(c => c.Id == id));

        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync(MedicineForCreationDTO medicineForCreationDTO )
        {
            var medicineForCreation = new MedicineForCreationDTO
            {
                Name = medicineForCreationDTO.Name,
                Description = medicineForCreationDTO.Description,
                Price = medicineForCreationDTO.Price,
                StoragePeriod = medicineForCreationDTO.StoragePeriod,
                CategoryId=medicineForCreationDTO.CategoryId
            };

            return Ok(await medicineService.CreateAsync(medicineForCreation));
        }

        [HttpDelete("{Id}")]
        public async ValueTask<IActionResult> DeleteAsync(int id)
            => Ok(await medicineService.DeleteAsync(c => c.Id == id));

        [HttpPut("{id}")]
        public async ValueTask<IActionResult> UpdateAsync(
            [FromRoute] int id,
            [FromQuery] MedicineForCreationDTO medicineForCreationDTO
          )
        {

            return Ok(await medicineService.UpdateAsync(id, medicineForCreationDTO));
        }
    }
}
