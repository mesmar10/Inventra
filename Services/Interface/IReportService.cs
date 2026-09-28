using Inventra.DTOs.Response;

namespace Inventra.Services.Interface
{
    public interface IReportService
    {
        Task<TotalSalesDto> GetTotalSalesAsync();
        Task<IEnumerable<TopCustomerDto>> GetTopCustomersAsync();
        Task<IEnumerable<TopProductDto>> GetTopProductsAsync();
        Task<IEnumerable<LowStockDto>> GetLowStockProductsAsync();
    }
}
