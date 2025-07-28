using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs {
    public class ProductResponseDto {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ProductDto Product { get; set; }
    }
}
