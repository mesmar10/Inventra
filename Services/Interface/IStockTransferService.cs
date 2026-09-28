using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.Models.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventra.Services.Interface
{
    public interface IStockTransferService
    {
      
        Task<StockTransferDto> CreateTransferAsync(StockTransferRequestDto dto);

     
        Task<IEnumerable<StockTransferDto>> GetAllTransfersAsync();

        Task<StockTransferDto> GetTransferByIdAsync(int transferId);
    }
}
