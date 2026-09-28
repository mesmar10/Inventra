using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.DTOs.Update;
using Inventra.Models.DomainModels;
using Inventra.Repository.Interface;
using Inventra.Services.Interface;

namespace Inventra.Services.Implementation
{
    public class CouponService : ICouponService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CouponService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CouponDto>> GetAllCouponsAsync()
        {
            var coupons = await _unitOfWork.Coupon.GetAllAsync();

            return coupons.Select(c => new CouponDto
            {
                Id = c.Id,
                Code = c.Code,
                DiscountPercentage = c.DiscountPercentage,
                IsActive = c.IsActive,
                ExpiryDate = c.ExpiryDate
            });
        }

        public async Task<CouponDto?> GetCouponByIdAsync(int id)
        {
            var coupon = await _unitOfWork.Coupon.GetByIdAsync(id);

            if (coupon == null)
                return null;

            return new CouponDto
            {
                Id = coupon.Id,
                Code = coupon.Code,
                DiscountPercentage = coupon.DiscountPercentage,
                IsActive = coupon.IsActive,
                ExpiryDate = coupon.ExpiryDate
            };
        }

        public async Task<CouponDto> CreateCouponAsync(CouponRequestDto dto)
        {
            var coupon = new Coupon
            {
                Code = dto.Code,
                DiscountPercentage = dto.DiscountPercentage,
                ExpiryDate = dto.ExpiryDate,
                IsActive = true
            };

            await _unitOfWork.Coupon.AddAsync(coupon);
            await _unitOfWork.SaveChangesAsync();

            return new CouponDto
            {
                Id = coupon.Id,
                Code = coupon.Code,
                DiscountPercentage = coupon.DiscountPercentage,
                IsActive = coupon.IsActive,
                ExpiryDate = coupon.ExpiryDate
            };
        }

        public async Task<bool> UpdateCouponAsync(int id, UpdateCouponDto dto)
        {
            var coupon = await _unitOfWork.Coupon.GetByIdAsync(id);

            if (coupon == null)
                return false;

            coupon.Code = dto.Code;
            coupon.DiscountPercentage = dto.DiscountPercentage;
            coupon.IsActive = dto.IsActive;
            coupon.ExpiryDate = dto.ExpiryDate;

            _unitOfWork.Coupon.UpdateAsync(coupon);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCouponAsync(int id)
        {
            var coupon = await _unitOfWork.Coupon.GetByIdAsync(id);

            if (coupon == null)
                return false;

            _unitOfWork.Coupon.DeleteAsync(coupon);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}