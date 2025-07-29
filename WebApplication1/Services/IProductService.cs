using WebApplication1.DTOs;

namespace WebApplication1.Services {
    /// <summary>
    /// Сервис для управления товарами
    /// </summary>
    public interface IProductService {
        /// <summary>
        /// Получает список всех товаров
        /// </summary>
        /// <returns>Список товаров</returns>
        Task<List<ProductDto>> GetAllProductsAsync();

        /// <summary>
        /// Получает товар по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор товара</param>
        /// <returns>DTO товара</returns>
        Task<ProductDto> GetProductByIdAsync(int id);

        /// <summary>
        /// Проверяет доступность всех товаров из заказа
        /// </summary>
        /// <param name="order">Детали заказа</param>
        /// <returns>True, если все товары доступны в нужном количестве</returns>
        Task<bool> IsAvailableAsync(OrderDetailsDto order);

        /// <summary>
        /// Резервирует товары, указанные в заказе
        /// </summary>
        /// <param name="order">Детали заказа</param>
        Task ReserveProductsAsync(OrderDetailsDto order);
    }

}
