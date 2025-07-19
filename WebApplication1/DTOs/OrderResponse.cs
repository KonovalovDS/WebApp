namespace WebApplication1.DTOs {
    public class OrderResponse {
        public Guid OrderId { get; set; }
        public decimal Total { get; set; }
        public string Message { get; set; } = null!;
    }
}
