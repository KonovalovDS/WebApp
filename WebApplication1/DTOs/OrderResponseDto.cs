using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class OrderResponseDto {
        [Required] public bool Success { get; set; }
        [Required] public string Message { get; set; }
        public OrderDto FeaturedOrder { get; set; } = null!;
    }
}
