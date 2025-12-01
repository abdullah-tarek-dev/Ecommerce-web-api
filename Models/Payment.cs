namespace Ecommerce.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Method { get; set; } = "";
        public string Status { get; set; } = "Pending";
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    }
}
