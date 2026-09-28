using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.Mapping;
using Inventra.Models.DomainModels;
using Inventra.Repository.Interface;
using Inventra.Services.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Inventra.Services
{
    public class StockTransferService : IStockTransferService
    {
        private readonly IGenericRepository<StockTransfer> _stockTransferRepository;
        private readonly IProductWarehouseRepository _productWarehouseRepository;

        public StockTransferService(
            IGenericRepository<StockTransfer> stockTransferRepository,
            IProductWarehouseRepository productWarehouseRepository
        )
        {
            _stockTransferRepository = stockTransferRepository;
            _productWarehouseRepository = productWarehouseRepository;
        }

        public async Task<StockTransferDto> CreateTransferAsync(StockTransferRequestDto dto)
        {
            // تحويل DTO إلى Domain Model
            var transfer = dto.ToStockTransferDomain();
            transfer.RequestDate = DateTime.UtcNow;
            transfer.Status = TransferStatus.Pending;
            transfer.CreatedByEmployeeId = 1008;

            // التحقق من الكميات في المخزن المصدر
            foreach (var detail in transfer.TransferDetails)
            {
                var sourceRecord = await _productWarehouseRepository
                    .GetByProductAndWarehouseAsync(detail.ProductId, transfer.FromWarehouseId);

                if (sourceRecord == null || sourceRecord.Quantity < detail.Quantity)
                    throw new System.InvalidOperationException($"Not enough stock for product {detail.ProductId}.");

                // خصم من المخزن المصدر
                sourceRecord.Quantity -= detail.Quantity;
                await _productWarehouseRepository.UpdateAsync(sourceRecord);

                // إضافة للمخزن الوجهة
                var destRecord = await _productWarehouseRepository
                    .GetByProductAndWarehouseAsync(detail.ProductId, transfer.ToWarehouseId);

                if (destRecord == null)
                {
                    destRecord = new ProductWarehouse
                    {
                        ProductId = detail.ProductId,
                        WarehouseId = transfer.ToWarehouseId,
                        Quantity = detail.Quantity,
                        BatchNumber = $"TR-{System.Guid.NewGuid().ToString().Substring(0, 6)}",
                        ExpiryDate = null,
                        ReorderLevel = 5
                    };
                    await _productWarehouseRepository.AddAsync(destRecord);
                }
                else
                {
                    destRecord.Quantity += detail.Quantity;
                    await _productWarehouseRepository.UpdateAsync(destRecord);
                }
            }

            // حفظ عملية التحويل
            await _stockTransferRepository.AddAsync(transfer);
            await _stockTransferRepository.SaveAsync();

            // إعادة تحميل مع الـ Includes
            var savedTransfer = await _stockTransferRepository.GetByIdAsync(
                transfer.Id,
                t => t.FromWarehouse,
                t => t.ToWarehouse,
                t => t.RequestedByEmployee,
                t => t.CreatedByEmployee,
                t => t.ReceivedByEmployee,
                t => t.TransferDetails
            );
          

            // تحويل إلى DTO للعرض
            return savedTransfer.ToStockTransferDto();
        }

        public async Task<IEnumerable<StockTransferDto>> GetAllTransfersAsync()
        {
            var transfers = await _stockTransferRepository.GetAllAsync(
                t => t.FromWarehouse,
                t => t.ToWarehouse,
                t => t.RequestedByEmployee,
                t => t.CreatedByEmployee,
                t => t.ReceivedByEmployee,
                t => t.TransferDetails
            );

            return transfers.Select(t => t.ToStockTransferDto()).ToList();
        }

        public async Task<StockTransferDto> GetTransferByIdAsync(int transferId)
        {
            var transfer = await _stockTransferRepository.GetByIdAsync(
                transferId,
                t => t.FromWarehouse,
                t => t.ToWarehouse,
                t => t.RequestedByEmployee,
                t => t.CreatedByEmployee,
                t => t.ReceivedByEmployee,
                t => t.TransferDetails
            );

            return transfer?.ToStockTransferDto();
        }
    }
}
