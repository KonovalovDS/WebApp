using Swashbuckle.AspNetCore.SwaggerGen;
using WebApplication1.DTOs;

namespace WebApplication1.Data {
    public class ProductStorage {
        private readonly Dictionary<int, Product> _products = new();
        public ProductStorage() {
            _products.Add(1, new Product { Name = "Ноутбук", Description = "Мощный ноутбук", Price = 1500, Quantity = 4 });
            _products.Add(2, new Product { Name = "Мышь", Description = "Игровая мышь", Price = 50, Quantity = 10 });
            _products.Add(3, new Product { Name = "Клавиатура", Description = "Механическая клавиатура", Price = 100, Quantity = 7 });
            Console.WriteLine("Init Storage");
        }

        public Dictionary<int, Product> GetProducts() {
            Console.WriteLine("Here you are");
            return _products;
        }

        public bool isAvailable(OrderRequest request) {
            foreach (var item in request.Items) {
                if (_products[item.ProductId].Quantity < item.Quantity) return false;
            }
            return true;
        }

        public void ReserveProducts(OrderRequest request) {
            foreach (var item in request.Items) {
                _products[item.ProductId].Quantity -= item.Quantity;
            }
        }
    }
}
