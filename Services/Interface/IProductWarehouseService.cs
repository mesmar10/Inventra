using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.DTOs.Update;

namespace Inventra.Services.Interface
{
    public interface IProductWarehouseService
    {
        // ✅ Get All
        Task<IEnumerable<ProductWarehouseDto>> GetAllAsync();

        // ✅ Get By Id
        Task<ProductWarehouseDto?> GetByIdAsync(int id);

        // ✅ Create
        Task AddAsync(ProductWarehouseRequestDto dto);

        // ✅ Update
        Task<bool> UpdateAsync(int id, ProductWarehouseUpdateDto dto);

        // ✅ Delete
        Task<bool> DeleteAsync(int id);
    }
}
