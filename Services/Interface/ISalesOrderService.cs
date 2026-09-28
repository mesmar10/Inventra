using Inventra.DTOs.Request;
using Inventra.DTOs.Response;

namespace Inventra.Services.Interface
{
    public interface ISalesOrderService
    {
        Task<SalesOrderDto> CreateOrderAsync(
            SalesOrderRequestDto dto,
            int employeeId);

        Task<IEnumerable<SalesOrderDto>>
            GetAllOrdersAsync();

        Task<bool> ApproveOrderAsync(int orderId);
        Task<bool> PayOrderAsync(int orderId, int deliveryDriverId);
    }
}
