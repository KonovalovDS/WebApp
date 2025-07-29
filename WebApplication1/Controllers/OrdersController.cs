using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase {
        private readonly OrderService _orderService;

        public OrdersController(OrderService service) {
            _orderService = service;
        }

        /// <summary>
        /// Создать заказ
        /// </summary>
        /// <returns>Информация о заказе</returns>
        /// <response code="200">Успешно создано</response>
        /// <response code="400">Не удалось создать</response>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDetailsDto request) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _orderService.CreateOrderAsync(request);
            if (!result.Success) return BadRequest(result.Message);
            return CreatedAtAction(nameof(GetById), new { id = result.FeaturedOrder.OrderId}, result);
        }

        /// <summary>
        /// Получить информацию о всех заказах
        /// </summary>
        /// <returns>Информация о заказах</returns>
        /// <response code="200">Успешно найдено</response>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllOrdersAsync() {
            var username = User.Identity?.Name;
            if (User.IsInRole("Admin")) return Ok(await _orderService.GetAllOrdersAsync());
            return Ok(await _orderService.GetAllOrdersByUsernameAsync(username));
        }

        /// <summary>
        /// Получить информацию о заказе
        /// </summary>
        /// <param name="id">ID заказа</param>
        /// <returns>Информация о заказе</returns>
        /// <response code="200">Успешно найдено</response>
        /// <response code="404">Заказ не найден</response>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) { 
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound("Такого заказа не существует");
            return Ok(order);
        }

        /// <summary>
        /// Удалить заказ
        /// </summary>
        /// <param name="id">ID заказа</param>
        /// <returns>Информация о заказе</returns>
        /// <response code="200">Успешно удалено</response>
        /// <response code="404">Заказ не найден</response>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(Guid id) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _orderService.DeleteOrderAsync(id);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
