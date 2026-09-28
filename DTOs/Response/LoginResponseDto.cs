using Inventra.Models.DomainModels;

namespace Inventra.DTOs.Response
{
    public class LoginResponseDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
