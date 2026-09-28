using Inventra.Models.DomainModels;

namespace Inventra.DTOs.Request
{
    public class StockTransferRequestDto
    {
        public int FromWarehouseId { get; set; }
        public int ToWarehouseId { get; set; }
        public int RequestedByEmployeeId { get; set; }
        public ICollection<StockTransferDetailRequestDto> TransferDetails { get; set; } = new List<StockTransferDetailRequestDto>();

    }
}
