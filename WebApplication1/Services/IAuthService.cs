using WebApplication1.DTOs;

namespace WebApplication1.Services {
    public interface IAuthService {
        UserDto Register(RegisterRequestDto request);
        string Login(LoginRequestDto request);
        UserDto? GetByUsername(string username);
        UserDto? TryAuthenticate(LoginRequestDto dto);
    }
}
