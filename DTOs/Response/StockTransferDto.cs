using Inventra.Models.DomainModels;

namespace Inventra.DTOs.Response
{
    public class StockTransferDto
    {
        public int Id { get; set; }
        public int FromWarehouseId { get; set; }
        public string FromWarehouse { get; set; } = string.Empty;
        public int ToWarehouseId { get; set; }
        public string ToWarehouse { get; set; } = string.Empty;
        public int RequestedByEmployeeId { get; set; }
        public string RequestedByEmployee { get; set; } = string.Empty;
        public int? CreatedByEmployeeId { get; set; }
        public string CreatedByEmployee { get; set; } = string.Empty;
        public int? ReceivedByEmployeeId { get; set; }
        public string ReceivedByEmployee { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }
        public DateTime? DispatchDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public TransferStatus Status { get; set; }
        public ICollection<StockTransferDetailDto> TransferDetails { get; set; } = new List<StockTransferDetailDto>();
    }
}
