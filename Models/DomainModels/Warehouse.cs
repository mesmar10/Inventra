namespace Inventra.Models.DomainModels
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int? ManagerId { get; set; }
        public Employee? Manager { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<ProductWarehouse> ProductWarehouses { get; set; } = new List<ProductWarehouse>();
        public ICollection<SalesOrder> SalesOrders { get; set; }= new List<SalesOrder>();
    }
}
