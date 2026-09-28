using Inventra.Models.DomainModels;

namespace Inventra.DTOs.Request
{
    public class SalesOrderRequestDto
    {
        public int CustomerId { get; set; }

        public int WarehouseId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public string? CouponCode { get; set; }

        public List<SalesOrderItemRequestDto> Items { get; set; } = new();
    }
}
