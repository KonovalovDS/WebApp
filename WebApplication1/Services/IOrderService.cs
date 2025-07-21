using WebApplication1.DTOs;
using WebApplication1.Models;


namespace WebApplication1.Services {
    public interface IOrderService {
        List<OrderDto> GetAllOrders();
        OrderDto GetOrderById(Guid id);
        OrderResponseDto CreateOrder(OrderDetailsDto order);
    }
}
