namespace Inventra.DTOs.Update
{
    public class UpdateCouponDto
    {
        public string Code { get; set; } = string.Empty;

        public decimal DiscountPercentage { get; set; }

        public bool IsActive { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
