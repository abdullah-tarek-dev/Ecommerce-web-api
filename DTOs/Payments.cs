namespace Ecommerce.DTOs
{

    public class PaymentRequestDto
    {
        public int OrderId { get; set; }
        public string Method { get; set; } = ""; // e.g., "CreditCard", "PayPal"
    }

    public class PaymentResponseDto
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public string Status { get; set; } = ""; // e.g., "Pending", "Completed"
        public DateTime PaymentDate { get; set; }
        public string Method { get; set; } = "";
    }
}
