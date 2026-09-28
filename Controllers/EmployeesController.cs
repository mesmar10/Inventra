using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.DTOs.Update;
using Inventra.Services.Interface;

namespace Inventra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,WarehouseManager")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,WarehouseManager")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById([FromRoute] int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound($"Employee with Id {id} was not found.");
            }
            return Ok(employee);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,WarehouseManager")]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromBody] EmployeeRequestDto employeeRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdEmployee = await _employeeService.CreateEmployeeAsync(employeeRequestDto);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.Id }, createdEmployee);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,WarehouseManager")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int id, [FromBody] EmployeeUpdateDto employeeUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = await _employeeService.UpdateEmployeeAsync(id, employeeUpdateDto);
            if (!isUpdated)
            {
                return NotFound($"Employee with Id {id} was not found.");
            }

            return Ok("Employee updated successfully");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            var isDeleted = await _employeeService.DeleteEmployeeAsync(id);
            if (!isDeleted)
            {
                return NotFound($"Employee with Id {id} was not found.");
            }

            return Ok("Employee deleted successfully");
        }
        [HttpPut("{id}/change-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole(int id,EmployeeRequestDto dto)
        {
            var result =
                await _employeeService.ChangeRoleAsync(id,dto.Role);

            if (!result)
                return NotFound();

            return Ok("Role updated successfully");
        }
    }
}