using Inventra.Data;
using Inventra.Models.DomainModels;
using Inventra.Repository.Interface;

namespace Inventra.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventraDbContext dbContext;

        public UnitOfWork(InventraDbContext dbContext)
        {
            this.dbContext = dbContext;
            Warehouse = new GenericRepository<Warehouse>(dbContext);
            Employee = new GenericRepository<Employee>(dbContext);
            Product = new GenericRepository<Product>(dbContext);
            ProductWarehouse = new GenericRepository<ProductWarehouse>(dbContext);
            StockTransfer = new GenericRepository<StockTransfer>(dbContext);
            StockTransferDetail = new GenericRepository<StockTransferDetail>(dbContext);
            Customer = new GenericRepository<Customer>(dbContext);
            SalesOrder = new GenericRepository<SalesOrder>(dbContext);
            SalesOrderItem = new GenericRepository<SalesOrderItem>(dbContext);
            Coupon = new GenericRepository<Coupon>(dbContext);

        }


        public IGenericRepository<Warehouse> Warehouse { get; private set; }

        public IGenericRepository<Employee> Employee { get; private set; }

        public IGenericRepository<Product> Product { get; private set; }

        public IGenericRepository<ProductWarehouse> ProductWarehouse { get; private set; }

        public IGenericRepository<StockTransfer> StockTransfer { get; private set; }

        public IGenericRepository<StockTransferDetail> StockTransferDetail { get; private set; }
        public IGenericRepository<Customer> Customer { get; private set; }
        public IGenericRepository<SalesOrder> SalesOrder { get; }

        public IGenericRepository<SalesOrderItem> SalesOrderItem { get; }
        public IGenericRepository<Coupon> Coupon { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
