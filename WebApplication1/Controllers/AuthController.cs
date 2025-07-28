using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.DTOs;

namespace WebApplication1.Controllers {

    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;

        public AuthController(IAuthService authService, IJwtService jwtService) {
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto) {
            try {
                var user = await _authService.RegisterAsync(dto);
                var token = _jwtService.GenerateToken(user);
                return Ok(new { token });
            }
            catch (Exception ex) {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto) {
            var user = await _authService.TryAuthenticateAsync(dto);
            if (user == null)
                return Unauthorized(new { message = "Invalid credentials" });

            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
