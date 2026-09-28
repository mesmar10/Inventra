namespace Inventra.DTOs.Response
{
    public class WarehouseDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int? ManagerId { get; set; }
        public string ManagerName { get; set; } = string.Empty;
        public List<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();
        public List<ProductWarehouseDto> ProductWarehouses { get; set; } = new List<ProductWarehouseDto>();
    }
}
