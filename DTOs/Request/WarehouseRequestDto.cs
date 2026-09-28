namespace Inventra.DTOs.Request
{
    public class WarehouseRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int? ManagerId { get; set; }
    }
}
