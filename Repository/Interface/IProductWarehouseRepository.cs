using Inventra.Models.DomainModels;

namespace Inventra.Repository.Interface
{
    public interface IProductWarehouseRepository : IGenericRepository<ProductWarehouse>
    {
        Task<ProductWarehouse?> GetByProductAndWarehouseAsync(int productId, int warehouseId);
    }
}
