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

        public AuthService(AppDbContext dbContext, IPasswordHasher<User> passwordHasher, IJwtService tokenService) {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<UserDto> RegisterAsync(RegisterRequestDto request) {
            if (await _dbContext.Users.AnyAsync(u => u.Username == request.Username)) throw new Exception("User already exists");

            var newUser = new User { Username = request.Username };
            var passwordHash = _passwordHasher.HashPassword(newUser, request.Password);
            var user = UserMapper.ToModel(request, passwordHash);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return UserMapper.ToDto(user);
        }

        public async Task<string> LoginAsync(LoginRequestDto request) {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null) throw new Exception("Неверное имя пользователя или пароль");
            var result = _passwordHasher.VerifyHashedPassword(user, user.HashPassword, request.Password);
            if (result == PasswordVerificationResult.Failed) throw new Exception("Неверное имя пользователя или пароль");
            return _tokenService.GenerateToken(UserMapper.ToDto(user));
        }

        public async Task<bool> ValidateCredentialsAsync(LoginRequestDto request) {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null) return false;
            var result = _passwordHasher.VerifyHashedPassword(user, user.HashPassword, request.Password);
            return result == PasswordVerificationResult.Success;
        }

        public async Task<UserDto?> GetByUsernameAsync(string username) {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user != null ? UserMapper.ToDto(user) : null;
        }

        public async Task<UserDto?> TryAuthenticateAsync(LoginRequestDto dto) {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (user == null) return null;
            var result = _passwordHasher.VerifyHashedPassword(user, user.HashPassword, dto.Password);
            if (result != PasswordVerificationResult.Success) return null;
            return UserMapper.ToDto(user);
        }
    }

}
