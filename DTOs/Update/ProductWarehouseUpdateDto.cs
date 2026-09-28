namespace Inventra.DTOs.Update
{
    public class ProductWarehouseUpdateDto
    {
        public decimal Quantity { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? BatchNumber { get; set; }
        public double ReorderLevel { get; set; }
    }
}
