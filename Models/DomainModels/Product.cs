namespace Inventra.Models.DomainModels
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal CostPrice { get; set; } 
        public decimal SellingPrice { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int UnitId { get; set; }
        public Unit? Unit { get; set; }
        public string? Sku { get; set; } // رمز الباركود أو الكود التعريفي
        public ICollection<SalesOrderItem> SalesOrderItems { get; set; }= new List<SalesOrderItem>();
    }
}
