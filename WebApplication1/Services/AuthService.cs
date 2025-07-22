using WebApplication1.Data;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;
using WebApplication1.DTOs;
using WebApplication1.Mappers;

namespace WebApplication1.Services {
    public class AuthService : IAuthService {
        private readonly UserStorage _userStorage;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _tokenService;

        public AuthService(UserStorage userStorage, IPasswordHasher<User> passwordHasher, IJwtService tokenService) {
            _userStorage = userStorage;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public UserDto Register(RegisterRequestDto request) {
            if (_userStorage.ExistsByUsername(request.Username)) throw new Exception("User already exists");
            var newUser = new User { Username = request.Username };
            var passwordHash = _passwordHasher.HashPassword(newUser, request.Password);
            var user = UserMapper.ToModel(request, passwordHash);
            _userStorage.Add(user);
            return UserMapper.ToDto(user);
        }

        public string Login(LoginRequestDto request) {
            var user = _userStorage.FindByUsername(request.Username);
            if (user == null) throw new Exception("Неверное имя пользователя или пароль");
            var result = _passwordHasher.VerifyHashedPassword(user, user.HashPassword, request.Password);
            if (result == PasswordVerificationResult.Failed) throw new Exception("Неверное имя пользователя или пароль");
            return _tokenService.GenerateToken(UserMapper.ToDto(user));
        }

        public bool ValidateCredentials(LoginRequestDto request) {
            var user = _userStorage.FindByUsername(request.Username);
            if (user == null) return false;
            var result = _passwordHasher.VerifyHashedPassword(user, user.HashPassword, request.Password);
            return result == PasswordVerificationResult.Success;
        }

        public UserDto? GetByUsername(string username) {
            var user = _userStorage.FindByUsername(username);
            return user != null ? UserMapper.ToDto(user) : null;
        }

        public UserDto? TryAuthenticate(LoginRequestDto dto) {
            var user = _userStorage.FindByUsername(dto.Username);
            if (user == null) return null;
            var result = _passwordHasher.VerifyHashedPassword(user, user.HashPassword, dto.Password);
            if (result != PasswordVerificationResult.Success) return null;
            return UserMapper.ToDto(user);
        }
    }
}
