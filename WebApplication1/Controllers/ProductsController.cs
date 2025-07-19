using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers {
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase {
        private readonly OrderService _service;

        [HttpGet]
        public IActionResult GetAll() {
            return Ok(_service.GetProducts());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var product = _service.GetProducts()[id];
            if (product == null) return NotFound("Товар не найден");
            return Ok(product);
        }
    }
}
