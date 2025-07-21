using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Properties;
using WebApplication1.Data;

namespace WebApplication1.Services {
    public class AuthService : IAuthService {
        private readonly JwtOptions _jwtOptions;
        private readonly UserStorage _userStorage;

        public AuthService(IOptions<JwtOptions> options, UserStorage userStorage) {
            _jwtOptions = options.Value;
            _userStorage = userStorage;
        }

        public bool ValidateUser(string username, string password) {
            var user = _userStorage.GetByUsername(username);
            if (user == null) return false;
            return user.Password == password;
        }

        public string GenerateToken(string username, string role) {
            var claims = new[] {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
