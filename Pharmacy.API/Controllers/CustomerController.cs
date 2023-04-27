using Microsoft.AspNetCore.Mvc;
using Pharmacy.Domain.Configurations;
using Pharmacy.Service.DTOs;
using Pharmacy.Service.Interfaces;

namespace Pharmacy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }
        [HttpGet]
        public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
        {
            return Ok(await customerService.GetAllAsync(@params));
        }

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
            => Ok(await customerService.GetAsync(c => c.Id == id));

        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync(CustomerForCreationDTO customerForCreationDTO)
        {
            var CustomerForCreationDTO = new CustomerForCreationDTO
            {
               FirstName=customerForCreationDTO.FirstName,
               LastName=customerForCreationDTO.LastName,
               Address=customerForCreationDTO.Address,
               PhoneNumber=customerForCreationDTO.PhoneNumber
            };

            return Ok(await customerService.CreateAsync(CustomerForCreationDTO));
        }

        [HttpDelete("{Id}")]
        public async ValueTask<IActionResult> DeleteAsync( int Id)
            => Ok(await customerService.DeleteAsync(c => c.Id == Id));

        [HttpPut("{id}")]
        public async ValueTask<IActionResult> UpdateAsync(
            [FromRoute] int id,
            [FromQuery] CustomerForCreationDTO customerForCreationDTO
           )
        {

            return Ok(await customerService.UpdateAsync(id, customerForCreationDTO));
        }
    }
}
