using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs {
    public class OrderRequestDto {
        [Required] OrderDetailsDto Details { get; set; }
    }
}
