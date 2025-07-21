using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs {
    public class ProductDto {
        [Required] public string Name { get; set; }
        public string Description { get; set; }
        [Required] public decimal Price { get; set; }
        [Required] public bool IsPriceIncluded { get; set; } = false;
        public decimal DiscountPrice { get; set; } = 0;
        [Required] public int Quantity { get; set; }
    }
}
