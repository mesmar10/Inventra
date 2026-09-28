using Inventra.Models.DomainModels;

namespace Inventra.Repository.Interface
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<Warehouse> Warehouse { get; }
        IGenericRepository<Employee> Employee { get; }
        IGenericRepository<Product> Product { get; }
        IGenericRepository<ProductWarehouse> ProductWarehouse { get; }
        IGenericRepository<StockTransfer> StockTransfer { get; }
        IGenericRepository<StockTransferDetail> StockTransferDetail { get; }
        IGenericRepository<Customer> Customer { get; }
        IGenericRepository<SalesOrder> SalesOrder { get; }
        IGenericRepository<SalesOrderItem> SalesOrderItem { get; }
        IGenericRepository<Coupon> Coupon { get; }

        Task<int> SaveChangesAsync();
    }
}
