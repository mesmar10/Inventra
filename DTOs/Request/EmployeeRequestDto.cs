using Inventra.Models.DomainModels;

namespace Inventra.DTOs.Request
{
    public class EmployeeRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public int? WarehouseId { get; set; }
        public UserRole Role { get; set; }
    }
}
