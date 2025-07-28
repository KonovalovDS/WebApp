using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }
        public OrderDetails Details { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }

    public class OrderDetails {
        public Customer Customer { get; set; }
        public Address ShippingAddress { get; set; }
        public List<OrderItem> Items { get; set; }
        public string? Notes { get; set; }
    }

    public class OrderItem {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
    }

    public class Customer {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class Address {
        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
    }
}
