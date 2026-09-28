namespace Inventra.DTOs.Response
{
    public class TotalSalesDto
    {
        public decimal TotalSales { get; set; }
    }
    public class TopCustomerDto
    {
        public string CustomerName { get; set; } = string.Empty;

        public int TotalOrders { get; set; }

        public decimal TotalSpent { get; set; }
    }
    public class TopProductDto
    {
        public string ProductName { get; set; } = string.Empty;

        public decimal SoldQuantity { get; set; }
    }
    public class LowStockDto
    {
        public string ProductName { get; set; } = string.Empty;

        public decimal Quantity { get; set; }

        public double ReorderLevel { get; set; }

        public string WarehouseName { get; set; } = string.Empty;
    }
}
