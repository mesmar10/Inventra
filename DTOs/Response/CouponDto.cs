namespace Inventra.DTOs.Response
{
    public class CouponDto
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public decimal DiscountPercentage { get; set; }

        public bool IsActive { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
