using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.Models.DomainModels;
using Inventra.Repository.Interface;
using Inventra.Services.Interface;
using Inventra.Mapping;

public class ProductService : IProductService
{
    private readonly IGenericRepository<Product> productRepository;

    public ProductService(IGenericRepository<Product> productRepository)
    {
        this.productRepository = productRepository;
    }

    // ✅ Get All
    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await productRepository.GetAllAsync(p => p.Unit);
        return products.Select(p => p.ToProductDto());
    }

    // ✅ Get By Id
    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id, p => p.Unit);
        return product?.ToProductDto();
    }

    // ✅ Create
    public async Task AddProductAsync(ProductRequestDto dto)
    {
        var product = dto.ToProductDoamin();
        await productRepository.AddAsync(product);
        await productRepository.SaveAsync();
    }

    // ✅ Update باستخدام ProductUpdateDto + Mapping
    public async Task<bool> UpdateProductAsync(int id, ProductUpdateDto dto)
    {
        var existingProduct = await productRepository.GetByIdAsync(id);
        if (existingProduct == null) return false;

        existingProduct.UpdateProductFromDto(dto);

        productRepository.UpdateAsync(existingProduct);
        await productRepository.SaveAsync();
        return true;
    }

    // ✅ Delete
    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product == null) return false;

        productRepository.DeleteAsync(product);
        await productRepository.SaveAsync();
        return true;
    }
}
