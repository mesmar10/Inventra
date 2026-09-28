namespace Inventra.DTOs.Update
{
    public class WarehouseUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int ManagerId { get; set; }
    }
}
