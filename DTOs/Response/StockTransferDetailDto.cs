namespace Inventra.DTOs.Response
{
    public class StockTransferDetailDto
    {
        public int Id { get; set; }
        public int StockTransferId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public Decimal Quantity { get; set; }
        public decimal PriceAtTransfer { get; set; }
    }
}
