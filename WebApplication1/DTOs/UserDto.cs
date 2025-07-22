namespace WebApplication1.DTOs {
    public class UserDto {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}
