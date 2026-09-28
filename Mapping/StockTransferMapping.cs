using NPOI.SS.Formula.Functions;
using System.Net.NetworkInformation;
using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.Models.DomainModels;

namespace Inventra.Mapping
{
    public static class StockTransferMapping
    {
        public static StockTransferDetail ToStockTransferDetailDomain(this StockTransferDetailRequestDto dto)
        {
            return new StockTransferDetail
            {
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                PriceAtTransfer = dto.PriceAtTransfer
            };
        }

        public static StockTransferDetailDto ToStockTransferDetailDto (this StockTransferDetail stockTranferDeatil)
        {
            return new StockTransferDetailDto
            {
                Id = stockTranferDeatil.Id,
                StockTransferId = stockTranferDeatil.StockTransferId,
                ProductId = stockTranferDeatil.ProductId,
                ProductName = stockTranferDeatil.Product.Name,
                Quantity = stockTranferDeatil.Quantity,
                PriceAtTransfer = stockTranferDeatil.PriceAtTransfer
            };
        }

        public static StockTransfer ToStockTransferDomain(this StockTransferRequestDto dto)
        {
            return new StockTransfer
            {
                FromWarehouseId = dto.FromWarehouseId,
                ToWarehouseId = dto.ToWarehouseId,
                RequestedByEmployeeId = dto.RequestedByEmployeeId,
                TransferDetails = dto.TransferDetails?.Select(t => t.ToStockTransferDetailDomain()).ToList() ?? new List<StockTransferDetail>()
            };
        }

        public static StockTransferDto ToStockTransferDto(this StockTransfer stockTranfer)
        {
            return new StockTransferDto
            {
                Id = stockTranfer.Id,
                FromWarehouseId = stockTranfer.FromWarehouseId,
                FromWarehouse = stockTranfer.FromWarehouse.Name?? "Unknown",
                ToWarehouseId = stockTranfer.ToWarehouseId,
                ToWarehouse = stockTranfer.ToWarehouse.Name?? "Unknown",
                RequestedByEmployeeId = stockTranfer.RequestedByEmployeeId,
                RequestedByEmployee = stockTranfer.RequestedByEmployee?.Name?? "Unknown",
                CreatedByEmployeeId = stockTranfer.CreatedByEmployeeId,
                CreatedByEmployee = stockTranfer.CreatedByEmployee?.Name?? "Unknown",
                ReceivedByEmployeeId = stockTranfer.ReceivedByEmployeeId,
                ReceivedByEmployee = stockTranfer.ReceivedByEmployee?.Name,
                RequestDate = stockTranfer.RequestDate,
                DispatchDate = stockTranfer.DispatchDate,
                DeliveryDate = stockTranfer.DeliveryDate,
                Status = stockTranfer.Status,
                TransferDetails = stockTranfer.TransferDetails?.Select(td => td.ToStockTransferDetailDto()).ToList()?? new List<StockTransferDetailDto>()
            };
        }
    }
}
