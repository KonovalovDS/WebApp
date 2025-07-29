using WebApplication1.DTOs;

namespace WebApplication1.Services {
    /// <summary>
    /// Сервис для регистрации и аутентификации пользователей
    /// </summary>
    public interface IAuthService {
        /// <summary>
        /// Регистрирует нового пользователя
        /// </summary>
        /// <param name="request">Данные для регистрации</param>
        /// <returns>DTO зарегистрированного пользователя</returns>
        Task<UserDto> RegisterAsync(RegisterRequestDto request);

        /// <summary>
        /// Аутентифицирует пользователя и возвращает JWT-токен
        /// </summary>
        /// <param name="request">Данные для входа</param>
        /// <returns>JWT-токен при успешной аутентификации</returns>
        Task<string> LoginAsync(LoginRequestDto request);

        /// <summary>
        /// Возвращает пользователя по имени
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <returns>DTO пользователя или null, если пользователь не найден</returns>
        Task<UserDto?> GetByUsernameAsync(string username);

        /// <summary>
        /// Проверяет учетные данные пользователя
        /// </summary>
        /// <param name="dto">Данные для входа</param>
        /// <returns>DTO пользователя при успешной аутентификации, иначе null</returns>
        Task<UserDto?> TryAuthenticateAsync(LoginRequestDto dto);
    }

}
