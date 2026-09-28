using Inventra.Models.DomainModels;

public class SalesOrder
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int WarehouseId { get; set; }

    public OrderStatus Status { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public PaymentMethod PaymentMethod { get; set; }

    public string? CouponCode { get; set; }

    public int CreatedByEmployeeId { get; set; }

    public Customer Customer { get; set; } = null!;

    public Warehouse Warehouse { get; set; } = null!;

    public Employee CreatedByEmployee { get; set; } = null!;
    public int? DeliveredByDriverId { get; set; }

    public Employee? DeliveredByDriver { get; set; }

    public DateTime? DeliveredAt { get; set; }

    public ICollection<SalesOrderItem> Items { get; set; } = new List<SalesOrderItem>();
    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
}