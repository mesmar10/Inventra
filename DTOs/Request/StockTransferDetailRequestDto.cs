namespace Inventra.DTOs.Request
{
    public class StockTransferDetailRequestDto
    {
        public int ProductId { get; set; }
        public Decimal Quantity { get; set; }
        public decimal PriceAtTransfer { get; set; }
    }
}
