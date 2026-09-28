using Microsoft.EntityFrameworkCore;
using Inventra.Models.DomainModels;

namespace Inventra.Data
{
    public class InventraDbContext : DbContext
    {
        public InventraDbContext(DbContextOptions<InventraDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProductWarehouse> ProductWarehouses { get; set; }
        public DbSet<StockTransfer> StockTransfers { get; set; }
        public DbSet<StockTransferDetail> StockTransferDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<SalesOrder> SalesOrders { get; set; }

        public DbSet<SalesOrderItem> SalesOrderItems { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().
            HasIndex(e => e.Email).IsUnique(); 
            // Ensure unique email addresses for employees

            modelBuilder.Entity<Unit>().HasData(
       new Unit { Id = 1, Name = "Piece" },
       new Unit { Id = 2, Name = "Box" },
       new Unit { Id = 3, Name = "Kg" }
   );
            // 1. حل مشكلة العلاقات المتعددة في الشحنات (StockTransfer -> Employee)
            modelBuilder.Entity<StockTransfer>()
                .HasOne(t => t.RequestedByEmployee)
                .WithMany()
                .HasForeignKey(t => t.RequestedByEmployeeId)
                .OnDelete(DeleteBehavior.Restrict); // لمنع الحذف المتتالي الكارثي

            modelBuilder.Entity<StockTransfer>()
                .HasOne(t => t.CreatedByEmployee)
                .WithMany()
                .HasForeignKey(t => t.CreatedByEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockTransfer>()
                .HasOne(t => t.ReceivedByEmployee)
                .WithMany()
                .HasForeignKey(t => t.ReceivedByEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // 2. حل مشكلة العلاقات المتعددة في المستودع والموظف (Warehouse <-> Employee)
            // العلاقة الأولى: الموظفين الذين يعملون في المستودع
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Warehouse)
                .WithMany(w => w.Employees)
                .HasForeignKey(e => e.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            // العلاقة الثانية: المدير المسؤول عن المستودع
            modelBuilder.Entity<Warehouse>()
                .HasOne(w => w.Manager)
                .WithMany()
                .HasForeignKey(w => w.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // 3. إعدادات إضافية لحماية حركات الشحنات والمخزون المزدوج
            modelBuilder.Entity<StockTransfer>()
                .HasOne(t => t.FromWarehouse)
                .WithMany()
                .HasForeignKey(t => t.FromWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StockTransfer>()
                .HasOne(t => t.ToWarehouse)
                .WithMany()
                .HasForeignKey(t => t.ToWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
            // 1. تحديد دقة الأسعار في كلاس Product (18 خانة كاملة، منها 3 خانات بعد الفاصلة)
            modelBuilder.Entity<Product>()
                .Property(p => p.CostPrice)
                .HasColumnType("decimal(18,3)");

            modelBuilder.Entity<Product>()
                .Property(p => p.SellingPrice)
                .HasColumnType("decimal(18,3)");

            // 2. تحديد دقة الكمية في كلاس ProductWarehouse (تسمح بأوزان دقيقة مثل 0.250 كيلو)
            modelBuilder.Entity<ProductWarehouse>()
                .Property(pw => pw.Quantity)
                .HasColumnType("decimal(18,3)");

            modelBuilder.Entity<ProductWarehouse>()
                .Property(pw => pw.ReorderLevel)
                .HasColumnType("decimal(18,3)");

            // 3. تحديد دقة الكمية والسعر لحظة النقل في كلاس StockTransferDetail
            modelBuilder.Entity<StockTransferDetail>()
                .Property(std => std.Quantity)
                .HasColumnType("decimal(18,3)");

            modelBuilder.Entity<StockTransferDetail>()
                .Property(std => std.PriceAtTransfer)
                .HasColumnType("decimal(18,3)");

            modelBuilder.Entity<SalesOrder>()
    .HasOne(o => o.Customer)
    .WithMany(c => c.SalesOrders)
    .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<SalesOrder>()
            .HasOne(o => o.Warehouse)
            .WithMany(w => w.SalesOrders)
            .HasForeignKey(o => o.WarehouseId);

            modelBuilder.Entity<SalesOrderItem>()
    .HasOne(i => i.SalesOrder)
    .WithMany(o => o.Items)
    .HasForeignKey(i => i.SalesOrderId);

            modelBuilder.Entity<SalesOrderItem>()
            .HasOne(i => i.Product)
            .WithMany(p => p.SalesOrderItems)
            .HasForeignKey(i => i.ProductId);

            modelBuilder.Entity<SalesOrder>()
    .HasOne(o => o.CreatedByEmployee)
    .WithMany()
    .HasForeignKey(o => o.CreatedByEmployeeId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesOrder>()
            .Property(o => o.TotalAmount)
            .HasColumnType("decimal(18,3)");

            modelBuilder.Entity<SalesOrder>()
    .HasOne(o => o.DeliveredByDriver)
    .WithMany()
    .HasForeignKey(o => o.DeliveredByDriverId)
    .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
