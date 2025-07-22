using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.DTOs;

namespace WebApplication1.Controllers {

    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase{
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;

        public AuthController(IAuthService authService, IJwtService jwtService) {
            _authService = authService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDto dto) {
            try {
                var user = _authService.Register(dto);
                var token = _jwtService.GenerateToken(user);
                return Ok(new { token });
            }
            catch (Exception ex) {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto dto) {
            var user = _authService.TryAuthenticate(dto);
            if (user == null) return Unauthorized(new { message = "Invalid credentials" });
            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
