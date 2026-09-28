using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.DTOs.Update;

namespace Inventra.Services.Interface
{
    public interface ICouponService
    {
        Task<IEnumerable<CouponDto>> GetAllCouponsAsync();

        Task<CouponDto?> GetCouponByIdAsync(int id);

        Task<CouponDto> CreateCouponAsync(CouponRequestDto dto);

        Task<bool> UpdateCouponAsync(int id, UpdateCouponDto dto);

        Task<bool> DeleteCouponAsync(int id);
    }
}