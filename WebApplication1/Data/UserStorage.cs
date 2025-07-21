using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class UserStorage {
        private readonly Dictionary<int, User> _users = new();

        public User? GetUser(int userId) => _users.TryGetValue(userId, out var user) ? user : null;
    }
}
