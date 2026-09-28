using Inventra.DTOs.Request;
using Inventra.DTOs.Response;

namespace Inventra.Services.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task AddProductAsync(ProductRequestDto dto);
        Task<bool> UpdateProductAsync(int id, ProductUpdateDto dto);
        Task<bool> DeleteProductAsync(int id);
    }
}
