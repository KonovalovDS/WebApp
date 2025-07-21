using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs {
    public class ProductsResponseDto {
        [Required] public List<ProductDto> Products { get; set; }
    }
}
