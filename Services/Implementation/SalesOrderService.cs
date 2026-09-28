using Inventra.Data;
using Inventra.DTOs.Request;
using Inventra.DTOs.Response;
using Inventra.Models.DomainModels;
using Inventra.Repository.Interface;
using Inventra.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Inventra.Services.Implementation
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly InventraDbContext _context;
        public SalesOrderService(IUnitOfWork unitOfWork,InventraDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<SalesOrderDto> CreateOrderAsync(SalesOrderRequestDto dto,int employeeId)
        {
            var customer = await _unitOfWork.Customer.GetByIdAsync(dto.CustomerId);

            if (customer == null)
                throw new Exception("Customer not found");

            var warehouse = await _unitOfWork.Warehouse.GetByIdAsync(dto.WarehouseId);

            if (warehouse == null)
                throw new Exception("Warehouse not found");

            decimal totalAmount = 0;

            foreach (var item in dto.Items)
            {
                var product = await _unitOfWork.Product.GetByIdAsync(item.ProductId);

                if (product == null)
                {
                    throw new Exception(
                        $"Product with Id {item.ProductId} not found");
                }

                totalAmount += product.SellingPrice * item.Quantity;
            }
            if (!string.IsNullOrWhiteSpace(dto.CouponCode))
            {
                var coupons = await _unitOfWork.Coupon.GetAllAsync();

                var coupon = coupons.FirstOrDefault(c =>
                    c.Code == dto.CouponCode &&
                    c.IsActive &&
                    c.ExpiryDate > DateTime.UtcNow);

                if (coupon != null)
                {
                    var discount =
                        totalAmount * (coupon.DiscountPercentage / 100);

                    totalAmount -= discount;
                }
            }


            var order = new SalesOrder
            {
                CustomerId = dto.CustomerId,
                WarehouseId = dto.WarehouseId,
                CreatedByEmployeeId = employeeId,
                Status = OrderStatus.Pending,
                PaymentStatus = PaymentStatus.Pending,
                PaymentMethod = dto.PaymentMethod,
                CouponCode = dto.CouponCode,
                TotalAmount = totalAmount,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.SalesOrder.AddAsync(order);

            await _unitOfWork.SaveChangesAsync();

            foreach (var item in dto.Items)
            {
                var product = await _unitOfWork.Product.GetByIdAsync(item.ProductId);

                var orderItem = new SalesOrderItem
                {
                    SalesOrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product!.SellingPrice
                };

                await _unitOfWork.SalesOrderItem
                    .AddAsync(orderItem);
            }

            await _unitOfWork.SaveChangesAsync();

            return new SalesOrderDto
            {
                Id = order.Id,
                CustomerName = customer.Name,
                TotalAmount = order.TotalAmount,
                Status = order.Status
            };
        }

        public async Task<IEnumerable<SalesOrderDto>> GetAllOrdersAsync()
        {
            var orders = await _context.SalesOrders.Include(o => o.Customer).ToListAsync();

            return orders.Select(o => new SalesOrderDto
            {
                Id = o.Id,
                CustomerName = o.Customer.Name,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                PaymentStatus = o.PaymentStatus
            });
        }

        public async Task<bool> ApproveOrderAsync(int orderId)
        {
            var order = await _unitOfWork.SalesOrder.GetByIdAsync(orderId);

            if (order == null)
                return false;

            if (order.Status != OrderStatus.Pending)
                return false;

            var orderItems = await _context.SalesOrderItems
                .Where(i => i.SalesOrderId == orderId)
                .ToListAsync();

            foreach (var item in orderItems)
            {
                var productWarehouse = await _context.ProductWarehouses
                    .FirstOrDefaultAsync(pw =>
                        pw.ProductId == item.ProductId &&
                        pw.WarehouseId == order.WarehouseId);

                if (productWarehouse == null)
                {
                    throw new Exception(
                        $"Product {item.ProductId} not found in warehouse");
                }

                if (productWarehouse.Quantity < item.Quantity)
                {
                    throw new Exception(
                        $"Insufficient stock for Product {item.ProductId}");
                }

                productWarehouse.Quantity -= item.Quantity;
                if (productWarehouse.Quantity <= (decimal)productWarehouse.ReorderLevel)
                {
                    await _context.Notifications.AddAsync(
                        new Notification
                        {
                            Message = $"Product ID {item.ProductId} is running low in stock.",
                            IsRead = false,
                            CreatedAt = DateTime.UtcNow
                        });
                }
            }

            order.Status = OrderStatus.Approved;

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PayOrderAsync(int orderId,int deliveryDriverId)
        {
            var order = await _unitOfWork.SalesOrder.GetByIdAsync(orderId);

            if (order == null)
                return false;

            order.PaymentStatus = PaymentStatus.Completed;

            order.DeliveredByDriverId = deliveryDriverId;

            order.DeliveredAt = DateTime.UtcNow;

            await _unitOfWork.SalesOrder.UpdateAsync(order);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
