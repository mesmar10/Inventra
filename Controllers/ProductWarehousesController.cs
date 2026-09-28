using Inventra.DTOs.Request;
using Inventra.DTOs.Update;
using Inventra.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.Controllers
{
    [Authorize(Roles = "Admin,WarehouseManager,WarehouseWorker")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductWarehousesController : ControllerBase
    {
        private readonly IProductWarehouseService service;

        public ProductWarehousesController(IProductWarehouseService service)
        {
            this.service = service;
        }

        // ✅ GET: api/ProductWarehouses
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await service.GetAllAsync();
            return Ok(list);
        }

        // ✅ GET: api/ProductWarehouses/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pw = await service.GetByIdAsync(id);
            if (pw == null) return NotFound();

            return Ok(pw);
        }

        // ✅ POST: api/ProductWarehouses
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductWarehouseRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await service.AddAsync(dto);
            return Ok("ProductWarehouse created successfully");
        }

        // ✅ PUT: api/ProductWarehouses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductWarehouseUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await service.UpdateAsync(id, dto);
            if (!updated) return NotFound();

            return Ok("ProductWarehouse updated successfully");
        }

        // ✅ DELETE: api/ProductWarehouses/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await service.DeleteAsync(id);
            if (!deleted) return NotFound();

            return Ok("ProductWarehouse deleted successfully");
        }
    }
}
