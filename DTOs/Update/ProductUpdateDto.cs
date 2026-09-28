namespace Inventra.DTOs.Request
{
    public class ProductUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string? Description { get; set; }
        public int UnitId { get; set; }
        public string? Sku { get; set; }
        public bool IsActive { get; set; }
    }
}
