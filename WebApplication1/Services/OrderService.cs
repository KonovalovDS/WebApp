using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Mappers;
using WebApplication1.DTOs;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services {
    public class OrderService : IOrderService {
        private readonly AppDbContext _dbContext;
        private readonly ProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(AppDbContext dbContext, ProductService productService, IHttpContextAccessor httpContextAccessor) {
            _dbContext = dbContext;
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync() {
            var orders = await _dbContext.Orders
                .Include(o => o.Details)
                    .ThenInclude(d => d.Items)
                .Include(o => o.Details)
                    .ThenInclude(d => d.Customer)
                .Include(o => o.Details)
                    .ThenInclude(d => d.ShippingAddress)
                .ToListAsync();

            return OrderMapper.ToDto(orders);
        }

        public async Task<OrderResponseDto> CreateOrderAsync(OrderDetailsDto orderDetailsDto) {
            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            if (username == null) {
                return new OrderResponseDto {
                    Success = false,
                    Message = "Unauthorized user"
                };
            }
            if (!await _productService.IsAvailableAsync(orderDetailsDto)) {
                return new OrderResponseDto {
                    Success = false,
                    Message = "Cannot create order, not enough products available"
                };
            }
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) {
                return new OrderResponseDto {
                    Success = false,
                    Message = "User not found"
                };
            }

            var items = OrderMapper.ToModel(orderDetailsDto).Items;
            decimal total = 0;
            foreach (var item in items) {
                var product = await _dbContext.Products.FindAsync(item.Id);
                if (product != null) {
                    total += product.Price * item.Quantity;
                }
            }
            var newOrder = new Order {
                OrderId = Guid.NewGuid(),
                Status = "Created",
                Total = total,
                CreatedAt = DateTime.Now,
                Details = OrderMapper.ToModel(orderDetailsDto),
                UserId = user.Id
            };
            await _productService.ReserveProductsAsync(orderDetailsDto);
            await _dbContext.Orders.AddAsync(newOrder);
            await _dbContext.SaveChangesAsync();
            return new OrderResponseDto {
                Success = true,
                Message = "Order successfully created",
                FeaturedOrder = OrderMapper.ToDto(newOrder)
            };
        }

        public async Task<OrderDto?> GetOrderByIdAsync(Guid id) {
            var order = await _dbContext.Orders
                .Include(o => o.Details)
                    .ThenInclude(d => d.Items)
                .Include(o => o.Details)
                    .ThenInclude(d => d.Customer)
                .Include(o => o.Details)
                    .ThenInclude(d => d.ShippingAddress)
                .FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null) return null;
            return OrderMapper.ToDto(order);
        }

        public async Task<List<OrderDto>> GetAllOrdersByUsernameAsync(string username) {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return new List<OrderDto>();
            var orders = await _dbContext.Orders
                .Where(o => o.UserId == user.Id)
                .Include(o => o.Details)
                    .ThenInclude(d => d.Items)
                .Include(o => o.Details)
                    .ThenInclude(d => d.Customer)
                .Include(o => o.Details)
                    .ThenInclude(d => d.ShippingAddress)
                .ToListAsync();

            return OrderMapper.ToDto(orders);
        }

        public async Task<OrderResponseDto> DeleteOrderAsync(Guid id) {
            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            if (username == null) {
                return new OrderResponseDto {
                    Success = false,
                    Message = "Unauthorized user"
                };
            }
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) {
                return new OrderResponseDto {
                    Success = false,
                    Message = "User not found"
                };
            }
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null) {
                return new OrderResponseDto {
                    Success = false,
                    Message = "Order not found"
                };
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
            return new OrderResponseDto {
                Success = true,
                Message = "Order successfully deleted"
            };
        }
    }
}
