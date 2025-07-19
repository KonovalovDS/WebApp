using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers {
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase {
        private readonly OrderService _service;

        public OrdersController(OrderService service) {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] OrderRequest request) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _service.CreateOrder(request);
            return CreatedAtAction(nameof(GetById), new { id = result.OrderId }, result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id) { 
            var order = _service.GetOrderStatus(id);
            if (order == null) return NotFound("Такого заказа не существует");
            return Ok(order);
        }   
    }
}
