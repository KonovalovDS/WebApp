using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class OrderStorage {
        private readonly Dictionary<Guid, Order> _orders = new();

        public OrderStorage() { }

        public List<Order> GetAllOrders() => _orders.Values.ToList();

        public void SaveOrder(Guid id, Order order) => _orders[id] = order;

        public Order? GetOrder(Guid id) => _orders.TryGetValue(id, out var order) ? order : null;
    }
}
