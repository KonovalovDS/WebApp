using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.Mappers;

namespace WebApplication1.Controllers {
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase {
        private readonly ProductService _service;

        public ProductController(ProductService service) {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() {
            return Ok(_service.GetAllProducts());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var product = _service.GetProductById(id);
            if (product == null) return NotFound("Товар не найден");
            return Ok(product);
        }
    }
}
