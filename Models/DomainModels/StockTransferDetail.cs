namespace Inventra.Models.DomainModels
{
    public class StockTransferDetail
    {
        public int Id { get; set; }
        public int StockTransferId { get; set; }
        public StockTransfer? StockTransfer { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public Decimal Quantity { get; set; }
        public decimal PriceAtTransfer { get; set; }
    }
}
