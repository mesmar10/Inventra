using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.DTOs.Update;
using Inventra.Mapping;
using Inventra.Models.DomainModels;
using Inventra.Repository.Implementation;
using Inventra.Repository.Interface;
using Inventra.Services.Interface;

namespace Inventra.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IGenericRepository<Warehouse> _warehouseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IGenericRepository<Employee> employeeRepository,
        IGenericRepository<Warehouse> warehouseRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _warehouseRepository = warehouseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync(e => e.Warehouse);
            return employees.Select(e => e.ToEmployeeDto());
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id, e => e.Warehouse);
            return employee?.ToEmployeeDto();
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(EmployeeRequestDto employeeDto)
        {
            var entity = employeeDto.ToEmployeeDomain();
            var emailExists = await _employeeRepository.ExistsAsync(e => e.Email == employeeDto.Email);
            if (emailExists)
                throw new InvalidOperationException("Email already exists.");

            entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(employeeDto.Password);
            await _employeeRepository.AddAsync(entity);
            await _employeeRepository.SaveAsync();
            // 🔑 Extra logic: auto-assign manager to warehouse
            if (entity.Role == UserRole.WarehouseManager && entity.WarehouseId != null)
            {
                var warehouse = await _warehouseRepository.GetByIdAsync(entity.WarehouseId.Value);
                if (warehouse != null)
                {
                    warehouse.ManagerId = entity.Id;
                    await _warehouseRepository.UpdateAsync(warehouse);
                    await _warehouseRepository.SaveAsync();
                }
            }
                return entity.ToEmployeeDto();
        }

        public async Task<bool> UpdateEmployeeAsync(int id, EmployeeUpdateDto employeeDto)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null)
                return false;

            var emailExists = await _employeeRepository.ExistsAsync(e => e.Email == employeeDto.Email && e.Id != id);
            if (emailExists)
                throw new InvalidOperationException("Email already exists.");

            existingEmployee.Name = employeeDto.Name;
            existingEmployee.Role = employeeDto.Role;
            existingEmployee.Email = employeeDto.Email;

            await _employeeRepository.UpdateAsync(existingEmployee);
            await _employeeRepository.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null)
                return false;

            await _employeeRepository.DeleteAsync(existingEmployee);
            await _employeeRepository.SaveAsync();
            return true;
        }
        public async Task<bool> ChangeRoleAsync(int employeeId,UserRole role)
        {
            var employee = await _unitOfWork.Employee.GetByIdAsync(employeeId);

            if (employee == null)
                return false;

            employee.Role = role;

            await _unitOfWork.Employee.UpdateAsync(employee);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}