using System.ComponentModel.DataAnnotations;
using WebApplication1.DTOs;

namespace WebApplication1.DTOs {
    public class OrderDto {
        [Required] public Guid OrderId { get; set; }
        [Required] public string Status { get; set; } = null!;
        public OrderDetailsDto Details { get; set; }
        [Required] public decimal Total { get; set; }
        [Required] public DateTime CreatedAt { get; set; }
    }
    public class OrderDetailsDto {
        [Required]
        public CustomerDto Customer { get; set; }
        [Required]
        public AddressDto ShippingAddress { get; set; } = null!;
        [Required]
        [MinLength(1, ErrorMessage = "Please, add items to order")]
        public List<OrderItemDto> Items { get; set; }
        public string? Notes { get; set; }
    }

    public class CustomerDto {
        [Required] public string Name { get; set; } = null!;
        [Required, EmailAddress] public string Email { get; set; } = null!;
    }

    public class AddressDto {
        [Required] public string Street { get; set; } = null!;
        [Required] public string City { get; set; } = null!;
        [Required] public string Zip { get; set; } = null!;
    }

    public class OrderItemDto {
        [Required] public int Id { get; set; }
        [Required] public int Quantity { get; set; }
    }
}
