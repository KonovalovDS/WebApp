using WebApplication1.DTOs;

namespace WebApplication1.Services {
    public interface IAuthService {
        Task<UserDto> RegisterAsync(RegisterRequestDto request);
        Task<string> LoginAsync(LoginRequestDto request);
        Task<UserDto?> GetByUsernameAsync(string username);
        Task<UserDto?> TryAuthenticateAsync(LoginRequestDto dto);
    }
}
