using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.Models.DomainModels;
using Inventra.Repository.Interface;
using Inventra.Services.Interface;

namespace Inventra.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<Employee> _employeeRepository;
        public AuthService(IGenericRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<Employee?> LoginAsync(LoginRequestDto request)
        {
            var employee = await _employeeRepository.FirstOrDefaultAsync(e => e.Email == request.Email);
            if (employee == null)
                return null;

            bool isPasswordValid =
            BCrypt.Net.BCrypt.Verify(request.Password, employee.PasswordHash);
            if (!isPasswordValid)
            {
                return null;
            }
            return employee;
        }
    }
}
