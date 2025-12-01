namespace Ecommerce.DTOs.Orders
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public int Qty { get; set; }
    }

    public class OrderCreateDto
    {
        public int UserId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }
}
