using Inventra.Models.DomainModels;

namespace Inventra.DTOs.Response
{
    public class SalesOrderDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }

   
}