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
        public async Task<IActionResult> GetAll() {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound("Товар не найден");
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteById(int id) {
            var result = await _productService.DeleteProductByIdAsync(id);
            if (!result.Success) return NotFound(result.Message);
            return Ok(result.Message);
        }
    }
}
