using WebApplication1.DTOs;

namespace WebApplication1.Services {
    /// <summary>
    /// Сервис для генерации JWT-токенов
    /// </summary>
    public interface IJwtService {
        /// <summary>
        /// Генерирует JWT-токен на основе данных пользователя
        /// </summary>
        /// <param name="user">DTO пользователя</param>
        /// <returns>Сгенерированный JWT-токен</returns>
        string GenerateToken(UserDto user);
    }

}
