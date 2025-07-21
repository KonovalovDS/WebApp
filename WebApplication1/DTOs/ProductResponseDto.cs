using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs {
    public class ProductResponseDto {
        [Required] ProductDto Product { get; set; }
    }
}
