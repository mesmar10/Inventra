using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.DTOs.Update;

namespace Inventra.Services.Interface
{
    public interface IWarehouseService
    {
        Task<IEnumerable<WarehouseDto>> GetAllWarehousesAsync();
        Task<WarehouseDetailsDto?> GetWarehouseByIdAsync(int Id);
        Task<WarehouseDto> CreateWarehouseAsync(WarehouseRequestDto warehouse);
        Task<bool> UpdateWarehouseAsync(int Id, WarehouseUpdateDto warehouse);
        Task<bool> DeleteWarehouseAsync(int Id);
    }
}
