using Inventra.Models.DomainModels;

namespace Inventra.DTOs.Response
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int UnitId { get; set; }
        public string UnitName { get; set; } = string.Empty;
        public string? Sku { get; set; }
    }
}
