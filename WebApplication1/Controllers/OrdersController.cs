using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Mappers;
using WebApplication1.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase {
        private readonly OrderService _orderService;

        public OrdersController(OrderService service) {
            _orderService = service;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] OrderDetailsDto request) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _orderService.CreateOrder(request);
            if (!result.Success) return BadRequest(result.Message);
            return CreatedAtAction(nameof(GetById), new { id = result.FeaturedOrder.OrderId}, result);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllOrders() {
            var username = User.Identity?.Name;
            if (User.IsInRole("Admin")) return Ok(_orderService.GetAllOrders());
            return Ok(_orderService.GetAllOrdersByUsername(username));
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id) { 
            var order = _orderService.GetOrderById(id);
            if (order == null) return NotFound("Такого заказа не существует");
            return Ok(order);
        }
    }
}
