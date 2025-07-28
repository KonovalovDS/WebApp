using WebApplication1.DTOs;
using WebApplication1.Models;


namespace WebApplication1.Services {
    public interface IOrderService {
        Task<List<OrderDto>> GetAllOrdersAsync();

        Task<List<OrderDto>> GetAllOrdersByUsernameAsync(string username);

        Task<OrderDto?> GetOrderByIdAsync(Guid id);

        Task<OrderResponseDto> CreateOrderAsync(OrderDetailsDto order);

        Task<OrderResponseDto> DeleteOrderAsync(Guid id);
    }
}
