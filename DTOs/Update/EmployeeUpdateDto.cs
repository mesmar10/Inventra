using Inventra.Models.DomainModels;

namespace Inventra.DTOs.Update
{
    public class EmployeeUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public int? WarehouseId { get; set; }
        public UserRole Role { get; set; }
    }
}
