using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Mappers {
    public static class OrderMapper {
        public static OrderDto ToDto(Order order) {
            return new OrderDto {
                OrderId = order.OrderId,
                Status = order.Status,
                Total = order.Total,
                CreatedAt = order.CreatedAt,
                Details = new OrderDetailsDto {
                    Customer = new CustomerDto {
                        Name = order.Details.Customer.Name,
                        Email = order.Details.Customer.Email
                    },
                    ShippingAddress = new AddressDto {
                        Street = order.Details.ShippingAddress.Street,
                        City = order.Details.ShippingAddress.City,
                        Zip = order.Details.ShippingAddress.Zip
                    },
                    Items = order.Details.Items.Select(i => new OrderItemDto {
                        Id = i.Id,
                        Quantity = i.Quantity
                    }).ToList(),
                    Notes = order.Details.Notes
                }
            };
        }

        public static List<OrderDto> toDto(List<Order> orders) {
            return orders.Select(order => ToDto(order)).ToList();
        }

        public static OrderDetails ToModel(OrderDetailsDto dto) {
            return new OrderDetails {
                Customer = new Customer {
                    Name = dto.Customer.Name,
                    Email = dto.Customer.Email
                },
                ShippingAddress = new Address {
                    Street = dto.ShippingAddress.Street,
                    City = dto.ShippingAddress.City,
                    Zip = dto.ShippingAddress.Zip
                },
                Items = dto.Items.Select(i => new OrderItem {
                    Id = i.Id,
                    Quantity = i.Quantity
                }).ToList(),
                Notes = dto.Notes
            };
        }
    }
}
