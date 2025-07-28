using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ProductStorage {
        private readonly Dictionary<int, Product> _products = new();
        public ProductStorage() {
            _products.Add(1, new Product { Name = "Ноутбук", Description = "Мощный ноутбук", Price = 1500, Quantity = 4 });
            _products.Add(2, new Product { Name = "Мышь", Description = "Игровая мышь", Price = 50, Quantity = 10 });
            _products.Add(3, new Product { Name = "Клавиатура", Description = "Механическая клавиатура", Price = 100, Quantity = 7 });
        }

        public Dictionary<int, Product> GetProducts() {
            return _products;
        }

        public bool IsAvailable(List<OrderItem> items) {
            foreach (var item in items) {
                if (!_products.TryGetValue(item.Id, out var product) || product.Quantity < item.Quantity) return false;
            }
            return true;
        }

        public void ReserveProducts(OrderDetails order) {
            foreach (var item in order.Items) {
                _products[item.Id].Quantity -= item.Quantity;
            }
        }

        public decimal GetPrice(int productId) {
            return _products[productId].Price;
        }

        public Product? GetProductById(int id) => _products.TryGetValue(id, out var product) ? product : null;

        public bool DeleteById(int id) => _products.Remove(id);
    }
}
