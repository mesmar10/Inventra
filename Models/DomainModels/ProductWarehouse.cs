namespace Inventra.Models.DomainModels
{
    public class ProductWarehouse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
        public Decimal Quantity { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? BatchNumber { get; set; }
        public double ReorderLevel { get; set; }
    }
}
