using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Mappers;
using WebApplication1.DTOs;

namespace WebApplication1.Services
{
    public class OrderService : IOrderService {
        private readonly OrderStorage _OrderStorage;
        private readonly ProductService _ProductService;

        public OrderService(OrderStorage orderStorage, ProductService productService) {
            _OrderStorage = orderStorage;
            _ProductService = productService;
        }

        public List<OrderDto> GetAllOrders() => OrderMapper.toDto(_OrderStorage.GetAllOrders());

        public OrderResponseDto CreateOrder(OrderDetailsDto order) {
            if (!_ProductService.IsAvailable(order)) {
                return new OrderResponseDto {
                    Success = false,
                    Message = "Cannot create order, not enough products available"
                };
            }

            var total = order.Items.Sum(i => _ProductService.GetProductById(i.Id).Price * i.Quantity);
            var id = Guid.NewGuid();

            var newOrder = new Order {
                OrderId = id,
                Status = "Created",
                Total = total,
                CreatedAt = DateTime.Now,
                Details = OrderMapper.ToModel(order)
            };
            _ProductService.ReserveProducts(order);
            _OrderStorage.SaveOrder(id, newOrder);
            return new OrderResponseDto {
                Success = true,
                Message = "Order successfully created",
                FeaturedOrder = OrderMapper.ToDto(newOrder)
            };
        }


        public OrderDto? GetOrderById(Guid id) => OrderMapper.ToDto(_OrderStorage.GetOrder(id));
    }
}
