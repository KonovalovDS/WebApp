using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs {
    public class OrderRequest {
        public CustomerDto Customer { get; set; } = null!;
        public AddressDto ShippingAddress { get; set; } = null!;
        public List<OrderItemDto> Items { get; set; } = null!;
        public string? Notes { get; set; }
    }

    public class CustomerDto {
        [Required]
        public string Name { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;
    }

    public class AddressDto {
        [Required] public string Street { get; set; } = null!;
        [Required] public string City { get; set; } = null!;
        [Required] public string Zip { get; set; } = null!;
    }

    public class OrderItemDto {
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }

}
