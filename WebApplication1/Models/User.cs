namespace WebApplication1.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public bool IsAdmin { get; set; } = false;
        public List<Order> Orders { get; set; } = new();
    }
}
