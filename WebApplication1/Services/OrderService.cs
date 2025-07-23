using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Mappers;
using WebApplication1.DTOs;
using System.Security.Claims;

namespace WebApplication1.Services
{
    public class OrderService : IOrderService {
        private readonly OrderStorage _orderStorage;
        private readonly ProductService _productService;
        private readonly UserStorage _userStorage;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(OrderStorage orderStorage, ProductService productService, UserStorage userStorage, IHttpContextAccessor httpContextAccessor) {
            _orderStorage = orderStorage;
            _productService = productService;
            _userStorage = userStorage;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<OrderDto> GetAllOrders() => OrderMapper.toDto(_orderStorage.GetAllOrders());

        public OrderResponseDto CreateOrder(OrderDetailsDto order) {
            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            if (username == null) {
                return new OrderResponseDto {
                    Success = false,
                    Message = "Unauthorized user"
                };
            }

            if (!_productService.IsAvailable(order)) {
                return new OrderResponseDto {
                    Success = false,
                    Message = "Cannot create order, not enough products available"
                };
            }

            var user = _userStorage.FindByUsername(username);
            if (user == null) {
                return new OrderResponseDto {
                    Success = false,
                    Message = "User not found"
                };
            }

            var total = order.Items.Sum(i => _productService.GetProductById(i.Id).Price * i.Quantity);
            var id = Guid.NewGuid();

            var newOrder = new Order {
                OrderId = id,
                Status = "Created",
                Total = total,
                CreatedAt = DateTime.Now,
                Details = OrderMapper.ToModel(order)
            };
            _productService.ReserveProducts(order);
            _orderStorage.SaveOrder(id, newOrder);
            user.OrdersIds.Add(id);
            return new OrderResponseDto {
                Success = true,
                Message = "Order successfully created",
                FeaturedOrder = OrderMapper.ToDto(newOrder)
            };
        }

        public OrderDto? GetOrderById(Guid id) => OrderMapper.ToDto(_orderStorage.GetOrder(id));

        public List<OrderDto> GetAllOrdersByUsername(string username) {
            List<Order> orders = new List<Order>();
            User user = _userStorage.FindByUsername(username);
            foreach (Guid id in user.OrdersIds) {
                orders.Add(_orderStorage.GetOrder(id));
            }
            return OrderMapper.toDto(orders);
        }
    }
}
