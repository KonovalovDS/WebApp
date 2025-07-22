using WebApplication1.DTOs;

namespace WebApplication1.Services {
    public interface IJwtService {
        string GenerateToken(UserDto user);
    }
}
