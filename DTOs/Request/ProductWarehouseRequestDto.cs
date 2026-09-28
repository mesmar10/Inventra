using Inventra.Models.DomainModels;

namespace Inventra.DTOs.Request
{
    public class ProductWarehouseRequestDto
    {
     
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public Decimal Quantity { get; set; }
        public string? BatchNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public double ReorderLevel { get; set; }
    }
}
