using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Mappers;
using WebApplication1.DTOs;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase {
        private readonly OrderService _service;

        public OrdersController(OrderService service) {
            _service = service;
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] OrderDetailsDto request) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _service.CreateOrder(request);
            if (!result.Success) return BadRequest(result.Message);
            return CreatedAtAction(nameof(GetById), new { id = result.FeaturedOrder.OrderId}, result);
        }

        [HttpGet]
        public IActionResult GetAllOrders() => Ok(_service.GetAllOrders());

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id) { 
            var order = _service.GetOrderById(id);
            if (order == null) return NotFound("Такого заказа не существует");
            return Ok(order);
        }
    }
}
