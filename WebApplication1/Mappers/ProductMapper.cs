using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Mappers {
    public static class ProductMapper {
        public static ProductDto ToDto(Product product) {
            return new ProductDto {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsPriceIncluded = product.IsPriceIncluded,
                DiscountPrice = product.DiscountPrice,
                Quantity = product.Quantity
            };
        }

        public static List<ProductDto> ToDtoList(List<Product> products) {
            return products.Select(p => ToDto(p)).ToList();
        }

        public static Product ToModel(ProductDto dto) {
            return new Product {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                IsPriceIncluded = dto.IsPriceIncluded,
                DiscountPrice = dto.DiscountPrice,
                Quantity = dto.Quantity
            };
        }
    }
}
