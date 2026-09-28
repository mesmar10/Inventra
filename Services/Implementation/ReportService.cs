using Microsoft.EntityFrameworkCore;
using Inventra.Data;
using Inventra.DTOs.Response;
using Inventra.Models.DomainModels;
using Inventra.Services.Interface;

namespace Inventra.Services.Implementation
{
    public class ReportService : IReportService
    {
        private readonly InventraDbContext _context;

        public ReportService(InventraDbContext context)
        {
            _context = context;
        }

        public async Task<TotalSalesDto> GetTotalSalesAsync()
        {
            var totalSales = await _context.SalesOrders
                .Where(o => o.PaymentStatus == PaymentStatus.Completed)
                .SumAsync(o => o.TotalAmount);

            return new TotalSalesDto
            {
                TotalSales = totalSales
            };
        }

        public async Task<IEnumerable<TopCustomerDto>>
    GetTopCustomersAsync()
        {
            return await _context.SalesOrders
                .Include(o => o.Customer)
                .Where(o => o.PaymentStatus == PaymentStatus.Completed)
                .GroupBy(o => o.Customer.Name)
                .Select(g => new TopCustomerDto
                {
                    CustomerName = g.Key,
                    TotalOrders = g.Count(),
                    TotalSpent = g.Sum(x => x.TotalAmount)
                })
                .OrderByDescending(x => x.TotalSpent)
                .Take(5)
                .ToListAsync();
        }


        public async Task<IEnumerable<TopProductDto>>
    GetTopProductsAsync()
        {
            return await _context.SalesOrderItems
                .Include(i => i.Product)
                .GroupBy(i => i.Product!.Name)
                .Select(g => new TopProductDto
                {
                    ProductName = g.Key,
                    SoldQuantity = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.SoldQuantity)
                .Take(5)
                .ToListAsync();
        }

        public async Task<IEnumerable<LowStockDto>>
    GetLowStockProductsAsync()
        {
            return await _context.ProductWarehouses
                .Include(pw => pw.Product)
                .Include(pw => pw.Warehouse)
                .Where(pw => pw.Quantity <= (decimal)pw.ReorderLevel)
                .Select(pw => new LowStockDto
                {
                    ProductName = pw.Product!.Name,
                    Quantity = pw.Quantity,
                    ReorderLevel = pw.ReorderLevel,
                    WarehouseName = pw.Warehouse!.Name
                })
                .ToListAsync();
        }
    }
}