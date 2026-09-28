using Microsoft.AspNetCore.Mvc;
using Inventra.DTOs;
using Inventra.DTOs.Request;
using Inventra.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Inventra.Controllers
{
    [Authorize(Roles = "Admin,WarehouseManager")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await productService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET: api/Products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await productService.AddProductAsync(dto);
            return Ok("Product created successfully");
        }

        // PUT: api/Products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductUpdateDto dto)
        {
            var updated = await productService.UpdateProductAsync(id, dto);
            if (!updated)
                return NotFound();

            return Ok("Product updated successfully");
        }

        // DELETE: api/Products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await productService.DeleteProductAsync(id);
            if (!deleted)
                return NotFound();

            return Ok("Product deleted successfully");
        }
    }
}
