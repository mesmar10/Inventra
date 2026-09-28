namespace Inventra.Models.DomainModels
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string? Email { get; set; }
        public ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();
    }
}
