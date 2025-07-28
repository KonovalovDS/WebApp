using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs {
    public class OrderRequestDto {
        [Required] public OrderDetailsDto Details { get; set; }
    }
}
