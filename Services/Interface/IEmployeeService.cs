using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.DTOs.Update;
using Inventra.Models.DomainModels;

namespace Inventra.Services.Interface
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<EmployeeDto> CreateEmployeeAsync(EmployeeRequestDto employeeDto);
        Task<bool> UpdateEmployeeAsync(int id, EmployeeUpdateDto employeeDto);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<bool> ChangeRoleAsync(int employeeId,UserRole role);
    }
}