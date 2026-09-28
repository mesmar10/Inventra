using Microsoft.AspNetCore.Mvc;
using Inventra.DTOs.Request;
using Inventra.Services.Interface;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Inventra.Controllers
{
    [Authorize(Roles = "Admin,WarehouseManager")]
    [ApiController]
    [Route("api/[controller]")]
    public class StockTransfersController : ControllerBase
    {
        private readonly IStockTransferService _stockTransferService;

        public StockTransfersController(IStockTransferService stockTransferService)
        {
            _stockTransferService = stockTransferService;
        }

        // POST: api/StockTransfers
        [HttpPost]
        public async Task<IActionResult> CreateTransfer([FromBody] StockTransferRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _stockTransferService.CreateTransferAsync(dto);
            return Ok(result);
        }

        // GET: api/StockTransfers
        [HttpGet]
        public async Task<IActionResult> GetAllTransfers()
        {
            var transfers = await _stockTransferService.GetAllTransfersAsync();
            return Ok(transfers);
        }

        // GET: api/StockTransfers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransferById(int id)
        {
            var transfer = await _stockTransferService.GetTransferByIdAsync(id);
            if (transfer == null)
                return NotFound();

            return Ok(transfer);
        }
    }
}
