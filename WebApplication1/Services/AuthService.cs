using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;
using WebApplication1.DTOs;
using WebApplication1.Mappers;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services {
    public class AuthService : IAuthService {
        private readonly AppDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _tokenService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(AppDbContext dbContext, IPasswordHasher<User> passwordHasher, IJwtService tokenService, ILogger<AuthService> logger) {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<UserDto> RegisterAsync(RegisterRequestDto request) {
            _logger.LogInformation("Регистрация пользователя: {Username}", request.Username);
            if (await _dbContext.Users.AnyAsync(u => u.Username == request.Username)) throw new Exception("User already exists");

            var newUser = new User { Username = request.Username };
            var passwordHash = _passwordHasher.HashPassword(newUser, request.Password);
            var user = UserMapper.ToModel(request, passwordHash);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Пользователь {Username} успешно зарегистрирован", request.Username);

            return UserMapper.ToDto(user);
        }

        public async Task<string> LoginAsync(LoginRequestDto request) {
            _logger.LogInformation("Попытка входа в систему пользователя: {Username}", request.Username);
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null) {
                _logger.LogInformation("Не удалось найти пользователя: {Username}", request.Username);
                throw new Exception("Неверное имя пользователя или пароль");
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.HashPassword, request.Password);
            if (result == PasswordVerificationResult.Failed) throw new Exception("Неверное имя пользователя или пароль");
            _logger.LogInformation("Успешный вход пользователя: {Username}", request.Username);
            return _tokenService.GenerateToken(UserMapper.ToDto(user));
        }

        public async Task<bool> ValidateCredentialsAsync(LoginRequestDto request) {
            _logger.LogInformation("Валидация данных пользователя: {Username}", request.Username);
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null) {
                _logger.LogInformation("Не удалось валидировать информацию о пользователе: {Username}", request.Username);
                return false;
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.HashPassword, request.Password);
            _logger.LogInformation("Успешная валидация данных о пользователе: {Username}", request.Username);
            return result == PasswordVerificationResult.Success;
        }

        public async Task<UserDto?> GetByUsernameAsync(string username) {
            _logger.LogInformation("Поиск учетной записи пользователя: {Username}", username);
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user != null ? UserMapper.ToDto(user) : null;
        }

        public async Task<UserDto?> TryAuthenticateAsync(LoginRequestDto dto) {
            _logger.LogInformation("Попытка аутентификации пользователя: {Username}", dto.Username);
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (user == null) {
                _logger.LogInformation("Не удалось найти пользователя пользователя: {Username}", dto.Username);
                return null;
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.HashPassword, dto.Password);
            if (result != PasswordVerificationResult.Success) return null;
            return UserMapper.ToDto(user);
        }
    }

}
