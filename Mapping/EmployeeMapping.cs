using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.Models.DomainModels;

namespace Inventra.Mapping
{
    public static class EmployeeMapping
    {
        public static Employee ToEmployeeDomain (this EmployeeRequestDto dto)
        {
            return new Employee
            {
                Name = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                WarehouseId = dto.WarehouseId,
                Role = dto.Role,
            };
        }
        public static EmployeeDto ToEmployeeDto(this Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                WarehouseId = employee.WarehouseId, 
                WarehouseName = employee.Warehouse?.Name,
                Role = employee.Role,
                IsActive = employee.IsActive,
                CreatedAt = employee.CreatedAt
            };
        }
    }
}
