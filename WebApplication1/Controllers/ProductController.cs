using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.DTOs;
using WebApplication1.Mappers;
using WebApplication1.Models;

namespace WebApplication1.Controllers {
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase {
        private readonly ProductService _productService;

        public ProductController(ProductService productService) {
            _productService = productService;
        }

        /// <summary>
        /// Получить все товары
        /// </summary>
        /// <returns>Информация о товарах</returns>
        /// <response code="200">Успешно найдено</response>
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Получить товар по ID
        /// </summary>
        /// <param name="id">ID товара</param>
        /// <returns>Информация о товаре</returns>
        /// <response code="200">Успешно найдено</response>
        /// <response code="404">Товар не найден</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound("Товар не найден");
            return Ok(product);
        }

        /// <summary>
        /// Добавить новый товар
        /// </summary>
        /// <returns>Информация о созданном товаре</returns>
        /// <response code="200">Успешно добавлено</response>
        /// <response code="400">Товар не добавлен</response>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] ProductDto dto) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var product = ProductMapper.ToModel(dto);
            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, ProductMapper.ToDto(product));
        }

        /// <summary>
        /// Удалить товар по ID
        /// </summary>
        /// <param name="id">ID товара</param>
        /// <returns>Информация о состоянии товара</returns>
        /// <response code="200">Успешно удалено</response>
        /// <response code="404">Товар не найден</response>
        [Authorize(Roles = "Admin")]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteById(int id) {
            var result = await _productService.DeleteProductByIdAsync(id);
            if (!result.Success) return NotFound(result.Message);
            return Ok(result.Message);
        }
    }
}
