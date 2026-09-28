namespace Inventra.Models.DomainModels
{
    public class StockTransfer
    {
        public int Id { get; set;  }
        public int FromWarehouseId { get; set; }
        public Warehouse? FromWarehouse { get; set; }
        public int ToWarehouseId { get; set; }
        public Warehouse? ToWarehouse { get; set; }
        public int RequestedByEmployeeId { get; set; }
        public Employee? RequestedByEmployee { get; set; }
        public int? CreatedByEmployeeId { get; set; }
        public Employee? CreatedByEmployee { get; set; }
        public int? ReceivedByEmployeeId { get; set; }
        public Employee? ReceivedByEmployee { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? DispatchDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public TransferStatus Status { get; set; }
        public ICollection<StockTransferDetail> TransferDetails { get; set; } = new List<StockTransferDetail>();
    }
}
