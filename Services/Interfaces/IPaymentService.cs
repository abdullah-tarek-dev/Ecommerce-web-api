using Ecommerce.DTOs;

namespace Ecommerce.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> ProcessPaymentAsync(PaymentRequestDto dto);
        Task<PaymentResponseDto?> GetPaymentByIdAsync(int id);
        Task<List<PaymentResponseDto>> GetAllPaymentsAsync();
    }
}
