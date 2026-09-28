using Inventra.Models.DomainModels;

namespace Inventra.DTOs.Response
{
    public class ProductWarehouseDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } = string.Empty;
        public Decimal Quantity { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? BatchNumber { get; set; }
        public double ReorderLevel { get; set; }
    }
}
