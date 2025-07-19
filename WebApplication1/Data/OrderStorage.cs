using WebApplication1.DTOs;

namespace WebApplication1.Data {
    public class OrderStorage {
        private readonly Dictionary<Guid, OrderStatusResponse> _orders = new();

        public void SaveOrder(Guid id, OrderStatusResponse order) => _orders[id] = order;

        public OrderStatusResponse? GetOrder(Guid id) => _orders.TryGetValue(id, out var order) ? order : null;
    }
}
