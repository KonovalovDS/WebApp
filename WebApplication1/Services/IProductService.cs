using WebApplication1.DTOs;

namespace WebApplication1.Services {
    public interface IProductService {
        List<ProductDto> GetAllProducts();
        ProductDto GetProductById(int id);
        bool IsAvailable(OrderDetailsDto order);
        void ReserveProducts(OrderDetailsDto order);
    }
}
