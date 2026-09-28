using Inventra.DTOs.Request;
using Inventra.DTOs.Update;
using Inventra.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.Controllers
{
    [Authorize(Roles = "Admin,WarehouseManager")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService){
            _customerService = customerService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            return Ok(await _customerService.GetAllCustomersAsync());
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer =
                await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCustomer(
    CustomerRequestDto dto)
        {
            var customer =
                await _customerService.CreateCustomerAsync(dto);

            return Ok(customer);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(
    int id,
    UpdateCustomerDto dto)
        {
            var result =
                await _customerService.UpdateCustomerAsync(id, dto);

            if (!result)
                return NotFound();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result =
                await _customerService.DeleteCustomerAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }



    }
}
