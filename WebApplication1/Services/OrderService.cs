using Microsoft.AspNetCore.Mvc.ViewFeatures;
using WebApplication1.Data;
using WebApplication1.DTOs;

namespace WebApplication1.Services {
    public class OrderService {
        private readonly OrderStorage _OrderStorage;
        private readonly ProductStorage _ProductStorage;

        public OrderService() {
            _OrderStorage = new OrderStorage();
            _ProductStorage = new ProductStorage();
            Console.WriteLine("Storages loaded");    
        }

        public Dictionary<int, Product> GetProducts() {
            Console.WriteLine("Give me products...");
            return _ProductStorage.GetProducts();
        }

        public bool OrderItemsAvailable(OrderRequest request) => _ProductStorage.isAvailable(request);
        
        public OrderResponse CreateOrder(OrderRequest request) {
            if (!_ProductStorage.isAvailable(request)) return new OrderResponse() { 
                Message = "Cannot create order, not enough products available"
            };
            
            var total = request.Items.Sum(i => GetPrice(i.ProductId) * i.Quantity);
            var id = Guid.NewGuid();
            _OrderStorage.SaveOrder(id, new OrderStatusResponse {
                OrderId = id,
                Status = "Created",
                Total = total,
                CreatedAt = DateTime.Now
            });
            return new OrderResponse {
                OrderId = id,
                Total = total,
                Message = "Order successfully created"
            };
        }

        public OrderStatusResponse? GetOrderStatus(Guid id) => _OrderStorage.GetOrder(id);

        private decimal GetPrice(int productId) => productId switch {
            1 => 99.9m,
            2 => 1488.0m,
            3 => 322m,
            _ => 0m
        };
    }
}
