using WebApplication1.DTOs;

namespace WebApplication1.Services {
    public interface IProductService {
        Task<List<ProductDto>> GetAllProductsAsync();

        Task<ProductDto> GetProductByIdAsync(int id);

        Task<bool> IsAvailableAsync(OrderDetailsDto order);

        Task ReserveProductsAsync(OrderDetailsDto order);
    }
}
