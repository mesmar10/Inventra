using Microsoft.AspNetCore.Mvc;
using Inventra.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Inventra.Controllers
{
[Authorize(Roles = "Admin,WarehouseManager")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("total-sales")]
        public async Task<IActionResult> GetTotalSales()
        {
            return Ok(
                await _reportService.GetTotalSalesAsync());
        }

        [HttpGet("top-customers")]
        public async Task<IActionResult> GetTopCustomers()
        {
            return Ok(
                await _reportService.GetTopCustomersAsync());
        }

        [HttpGet("top-products")]
        public async Task<IActionResult> GetTopProducts()
        {
            return Ok(
                await _reportService.GetTopProductsAsync());
        }

        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStockProducts()
        {
            return Ok(
                await _reportService.GetLowStockProductsAsync());
        }
    }
}