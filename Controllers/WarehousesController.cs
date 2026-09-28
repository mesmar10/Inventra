using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.DTOs.Update;
using Inventra.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Inventra.Controllers
{
[Authorize(Roles = "Admin,WarehouseManager")]
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseService WarehouseService;

        public WarehousesController(IWarehouseService WarehouseService)
        {
            this.WarehouseService = WarehouseService;
        }
        [HttpPost]
        public async Task<ActionResult<WarehouseDto>> CreateWarehouse([FromBody] WarehouseRequestDto warehouse)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var CreatedWarehouse = await WarehouseService.CreateWarehouseAsync(warehouse);
            return Ok(CreatedWarehouse);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarehouseDto>>> GetAllWarehouses()
        {
            var GetWarehouses = await WarehouseService.GetAllWarehousesAsync();
            return Ok(GetWarehouses);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<WarehouseDetailsDto>> GetWarehouseById(int id)
        {
            var Warehouse = await WarehouseService.GetWarehouseByIdAsync(id);
            if (Warehouse == null)
            {
                return NotFound("The Warehouse By ID {id} Is Not Found");
            }
            return Ok(Warehouse);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<WarehouseDto>> EditWarehouse(int id, [FromBody] WarehouseUpdateDto update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var WarehouseUpdated = await WarehouseService.UpdateWarehouseAsync(id, update);
            if (!WarehouseUpdated)
            {
                return NotFound("Not Found");
            }
            return Ok(WarehouseUpdated);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<WarehouseDto>> DeleteWarehouse(int Id)
        {
            var IsDeleted = await WarehouseService.DeleteWarehouseAsync(Id);
            if (!IsDeleted)
            {
                return NotFound("NotFound");
            }
            return Ok(IsDeleted);
            
        }
    }
}
