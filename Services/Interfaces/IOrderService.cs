using Ecommerce.DTOs.Orders;

namespace Ecommerce.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto dto);
        Task<OrderResponseDto?> GetOrderByIdAsync(int id);
        Task<List<OrderResponseDto>> GetAllOrdersAsync();
    }
}
