using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.DTOs.Update;
using Inventra.Models.DomainModels;

namespace Inventra.Mapping
{
    public static class ProductWarehouseMapping
    {
        // ✅ تحويل من RequestDto إلى Domain Model
        public static ProductWarehouse ToDomain(this ProductWarehouseRequestDto dto)
        {
            return new ProductWarehouse
            {
                ProductId = dto.ProductId,
                WarehouseId = dto.WarehouseId,
                Quantity = dto.Quantity,
                ExpiryDate = dto.ExpiryDate,
                BatchNumber = dto.BatchNumber,
                ReorderLevel = dto.ReorderLevel
            };
        }

        // ✅ تحويل من Domain Model إلى ResponseDto
        public static ProductWarehouseDto ToDto(this ProductWarehouse pw)
        {
            return new ProductWarehouseDto
            {
                Id = pw.Id,
                ProductId = pw.ProductId,
                ProductName = pw.Product?.Name ?? string.Empty,
                WarehouseId = pw.WarehouseId,
                WarehouseName = pw.Warehouse?.Name ?? string.Empty,
                Quantity = pw.Quantity,
                ExpiryDate = pw.ExpiryDate,
                BatchNumber = pw.BatchNumber,
                ReorderLevel = pw.ReorderLevel
            };
        }

        // ✅ تحديث الـ Domain Model من UpdateDto
        public static void UpdateFromDto(this ProductWarehouse pw, ProductWarehouseUpdateDto dto)
        {
            pw.Quantity = dto.Quantity;
            pw.ExpiryDate = dto.ExpiryDate;
            pw.BatchNumber = dto.BatchNumber;
            pw.ReorderLevel = dto.ReorderLevel;
        }
    }
}
