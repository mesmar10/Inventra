using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.DTOs.Update;
using Inventra.Models.DomainModels;
using Inventra.Repository.Interface;
using Inventra.Services.Interface;
using Inventra.Mapping;

namespace Inventra.Services
{
    public class ProductWarehouseService : IProductWarehouseService
    {
        private readonly IGenericRepository<ProductWarehouse> repository;

        public ProductWarehouseService(IGenericRepository<ProductWarehouse> repository)
        {
            this.repository = repository;
        }

        // ✅ Get All
        public async Task<IEnumerable<ProductWarehouseDto>> GetAllAsync()
        {
            var list = await repository.GetAllAsync(pw => pw.Product, pw => pw.Warehouse);
            return list.Select(pw => pw.ToDto());
        }

        // ✅ Get By Id
        public async Task<ProductWarehouseDto?> GetByIdAsync(int id)
        {
            var pw = await repository.GetByIdAsync(id, x => x.Product, x => x.Warehouse);
            return pw?.ToDto();
        }

        // ✅ Create
        public async Task AddAsync(ProductWarehouseRequestDto dto)
        {
            var pw = dto.ToDomain();
            await repository.AddAsync(pw);
            await repository.SaveAsync();
        }

        // ✅ Update
        public async Task<bool> UpdateAsync(int id, ProductWarehouseUpdateDto dto)
        {
            var pw = await repository.GetByIdAsync(id);
            if (pw == null) return false;

            pw.UpdateFromDto(dto);

            repository.UpdateAsync(pw);
            await repository.SaveAsync();
            return true;
        }

        // ✅ Delete
        public async Task<bool> DeleteAsync(int id)
        {
            var pw = await repository.GetByIdAsync(id);
            if (pw == null) return false;

            repository.DeleteAsync(pw);
            await repository.SaveAsync();
            return true;
        }
    }
}
