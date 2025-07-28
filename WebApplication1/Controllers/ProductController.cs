using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.DTOs;

namespace WebApplication1.Controllers {
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase {
        private readonly ProductService _productService;

        public ProductController(ProductService productService) {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll() {
            return Ok(_productService.GetAllProducts());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var product = _productService.GetProductById(id);
            if (product == null) return NotFound("Товар не найден");
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("id")]
        public IActionResult DeleteById(int id) {
            var result = _productService.DeleteProductById(id);
            if (!result.Success) return NotFound(result.Message);
            return Ok(result.Message);
        }
    }
}
