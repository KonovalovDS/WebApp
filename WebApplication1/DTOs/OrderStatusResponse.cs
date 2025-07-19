namespace WebApplication1.DTOs {
    public class OrderStatusResponse {
        public Guid OrderId { get; set; }
        public string Status { get; set; } = null!;
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
