using Inventra.DTOs.Request;
using Inventra.DTOs.Update;
using Inventra.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.Controllers
{
    [Authorize(Roles = "Admin,WarehouseManager")]
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoupons()
        {
            return Ok(await _couponService.GetAllCouponsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCouponById(int id)
        {
            var coupon = await _couponService.GetCouponByIdAsync(id);

            if (coupon == null)
                return NotFound();

            return Ok(coupon);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon(CouponRequestDto dto)
        {
            var coupon =
                await _couponService.CreateCouponAsync(dto);

            return Ok(coupon);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoupon(
            int id,
            UpdateCouponDto dto)
        {
            var result =
                await _couponService.UpdateCouponAsync(id, dto);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            var result =
                await _couponService.DeleteCouponAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}