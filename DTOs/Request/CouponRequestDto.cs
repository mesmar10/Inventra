namespace Inventra.DTOs.Request
{
    public class CouponRequestDto
    {
        public string Code { get; set; } = string.Empty;

        public decimal DiscountPercentage { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
