using Ecommerce.Data;
using Ecommerce.DTOs.Orders;
using Ecommerce.Models;
using Ecommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto dto)
        {
            var order = new Order
            {
                UserId = dto.UserId
            };

            foreach (var item in dto.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null) throw new Exception("Product not found");

                order.OrderItem.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Qty = item.Qty,
                    UnitPrice = product.Price
                });

                product.Stock -= item.Qty;
            }

            order.TotalAmount = order.OrderItem.Sum(i => i.UnitPrice * i.Qty);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return new OrderResponseDto
            {
                Id = order.Id,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                Items = order.OrderItem.Select(i => new OrderItemResponseDto
                {
                    ProductId = i.ProductId,
                    Qty = i.Qty,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }

        public async Task<OrderResponseDto?> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItem)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return null;

            return new OrderResponseDto
            {
                Id = order.Id,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                Items = order.OrderItem.Select(i => new OrderItemResponseDto
                {
                    ProductId = i.ProductId,
                    Qty = i.Qty,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }

        public async Task<List<OrderResponseDto>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItem)
                .Select(order => new OrderResponseDto
                {
                    Id = order.Id,
                    TotalAmount = order.TotalAmount,
                    OrderDate = order.OrderDate,
                    Items = order.OrderItem.Select(i => new OrderItemResponseDto
                    {
                        ProductId = i.ProductId,
                        Qty = i.Qty,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                }).ToListAsync();
        }
    }
}
