using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Mappers {
    public class UserMapper {
        public static UserDto ToDto(User user) => new UserDto {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            IsAdmin = user.IsAdmin
        };

        public static User ToModel(RegisterRequestDto dto, string passwordHash) => new User {
            Id = Guid.NewGuid(),
            Username = dto.Username,
            Email = dto.Email,
            HashPassword = passwordHash
        };
    }
}
