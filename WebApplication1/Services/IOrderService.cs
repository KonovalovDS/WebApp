using WebApplication1.DTOs;
using WebApplication1.Models;


namespace WebApplication1.Services {
    /// <summary>
    /// Сервис для управления заказами
    /// </summary>
    public interface IOrderService {
        /// <summary>
        /// Получает список всех заказов
        /// </summary>
        /// <returns>Список заказов</returns>
        Task<List<OrderDto>> GetAllOrdersAsync();

        /// <summary>
        /// Получает все заказы конкретного пользователя
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <returns>Список заказов пользователя</returns>
        Task<List<OrderDto>> GetAllOrdersByUsernameAsync(string username);

        /// <summary>
        /// Получает заказ по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>DTO заказа или null, если заказ не найден</returns>
        Task<OrderDto?> GetOrderByIdAsync(Guid id);

        /// <summary>
        /// Создает новый заказ
        /// </summary>
        /// <param name="order">Детали заказа</param>
        /// <returns>Результат создания заказа</returns>
        Task<OrderResponseDto> CreateOrderAsync(OrderDetailsDto order);

        /// <summary>
        /// Удаляет заказ по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Результат удаления заказа</returns>
        Task<OrderResponseDto> DeleteOrderAsync(Guid id);
    }

}
