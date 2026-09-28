using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Inventra.DTOs.Request;
using Inventra.Services.Interface;

namespace Inventra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {
        private readonly ISalesOrderService _salesOrderService;

        public SalesOrderController(
            ISalesOrderService salesOrderService)
        {
            _salesOrderService = salesOrderService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,WarehouseManager")]
        public async Task<IActionResult> CreateOrder(SalesOrderRequestDto dto)
        {
            var order =await _salesOrderService.CreateOrderAsync(dto, 1010);

            return Ok(order);
        }


        [HttpGet]
        [Authorize(Roles = "Admin,WarehouseManager,DeliveryDriver")]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(
                await _salesOrderService.GetAllOrdersAsync());
        }


        [HttpPut("{id}/approve")]
        [Authorize(Roles = "WarehouseManager")]
        public async Task<IActionResult> ApproveOrder(int id)
        {
            var result = await _salesOrderService.ApproveOrderAsync(id);

            if (!result)
                return BadRequest();

            return Ok("Order Approved Successfully");
        }
        [Authorize(Roles = "DeliveryDriver")]
        [HttpPut("{id}/pay")]
        public async Task<IActionResult> PayOrder(int id)
        {
            var employeeId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result =
                await _salesOrderService.PayOrderAsync(id, employeeId);

            if (!result)
                return NotFound();

            return Ok("Payment Completed Successfully");
        }

    }
}