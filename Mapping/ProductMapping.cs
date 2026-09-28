using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.Models.DomainModels;

namespace Inventra.Mapping
{
    public static class ProductMapping
    {
        public static Product ToProductDoamin(this ProductRequestDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                CostPrice = dto.CostPrice,
                SellingPrice = dto.SellingPrice,
                Description = dto.Description,
                UnitId = dto.UnitId,
                Sku = dto.Sku
            };
        }

        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                CostPrice = product.CostPrice,
                SellingPrice = product.SellingPrice,
                Description = product.Description,
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt,
                UnitId = product.UnitId,
                UnitName = product.Unit?.Name ?? string.Empty,
                Sku = product.Sku ?? string.Empty
            };
        }

        public static void UpdateProductFromDto(this Product product, ProductUpdateDto dto)
        {
            product.Name = dto.Name;
            product.CostPrice = dto.CostPrice;
            product.SellingPrice = dto.SellingPrice;
            product.Description = dto.Description;
            product.UnitId = dto.UnitId;
            product.Sku = dto.Sku;
            product.IsActive = dto.IsActive;
        }
    }
}
