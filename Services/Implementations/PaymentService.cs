using Ecommerce.Data;
using Ecommerce.DTOs;
using Ecommerce.Models;
using Ecommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PaymentResponseDto> ProcessPaymentAsync(PaymentRequestDto dto)
        {
            var order = await _context.Orders.FindAsync(dto.OrderId);
            if (order == null) throw new Exception("Order not found");

            var payment = new Payment
            {
                OrderId = dto.OrderId,
                Method = dto.Method,
                Status = "Completed",
                PaymentDate = DateTime.UtcNow
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return new PaymentResponseDto
            {
                PaymentId = payment.Id,
                OrderId = payment.OrderId,
                Status = payment.Status,
                PaymentDate = payment.PaymentDate,
                Method = payment.Method
            };
        }

        public async Task<PaymentResponseDto?> GetPaymentByIdAsync(int id)
        {
            var p = await _context.Payments.FindAsync(id);
            if (p == null) return null;

            return new PaymentResponseDto
            {
                PaymentId = p.Id,
                OrderId = p.OrderId,
                Status = p.Status,
                PaymentDate = p.PaymentDate,
                Method = p.Method
            };
        }

        public async Task<List<PaymentResponseDto>> GetAllPaymentsAsync()
        {
            return await _context.Payments
                .Select(p => new PaymentResponseDto
                {
                    PaymentId = p.Id,
                    OrderId = p.OrderId,
                    Status = p.Status,
                    PaymentDate = p.PaymentDate,
                    Method = p.Method
                })
                .ToListAsync();
        }
    }
}
