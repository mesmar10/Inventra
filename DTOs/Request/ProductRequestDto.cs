using Inventra.Models.DomainModels;

namespace Inventra.DTOs.Request
{
    public class ProductRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string? Description { get; set; }
        public int UnitId { get; set; }
        public string? Sku { get; set; }
    }
}
