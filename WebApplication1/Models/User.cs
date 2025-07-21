namespace WebApplication1.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public bool isAdmin { get; set; } = false;
        public List<Guid> OrdersIds { get; set; } = new();
    }
}
