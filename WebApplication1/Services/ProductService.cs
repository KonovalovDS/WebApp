using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Mappers;
using WebApplication1.DTOs;

namespace WebApplication1.Services {
    public class ProductService : IProductService{
        private readonly ProductStorage _ProductStorage;

        public ProductService(ProductStorage ProductStorage) {
            _ProductStorage = ProductStorage;
        }

        public List<ProductDto> GetAllProducts() {
            return _ProductStorage.GetProducts().Values.Select(p => ProductMapper.ToDto(p)).ToList();
        }

        public ProductDto? GetProductById(int id) {
            if (!_ProductStorage.GetProducts().TryGetValue(id, out var product)) return null;
            return ProductMapper.ToDto(product);
        }

        public bool IsAvailable(OrderDetailsDto order) => _ProductStorage.IsAvailable(OrderMapper.ToModel(order).Items);
        public void ReserveProducts(OrderDetailsDto order) => _ProductStorage.ReserveProducts(OrderMapper.ToModel(order));

        public ProductResponseDto DeleteProductById(int id) {
            if (!_ProductStorage.DeleteById(id)) {
                return new ProductResponseDto() { 
                    Success = false,
                    Message = "Product not found"
                };
            }
            return new ProductResponseDto() {
                Success = true,
                Message = "Product successfully deleted"
            };
        }
    }
}
