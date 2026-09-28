using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using Inventra.Data;
using Inventra.Models.DomainModels;
using Inventra.Repository.Interface;

namespace Inventra.Repository.Implementation
{
    public class ProductWarehouseRepository : GenericRepository<ProductWarehouse>, IProductWarehouseRepository
    {
        private readonly InventraDbContext dbContext;
        public ProductWarehouseRepository(InventraDbContext dbContext) :base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ProductWarehouse?> GetByProductAndWarehouseAsync(int productId, int warehouseId)
        {
            return await dbContext.ProductWarehouses.FirstOrDefaultAsync(pw => pw.ProductId == productId 
            && pw.WarehouseId == warehouseId);
        }
    }
}
