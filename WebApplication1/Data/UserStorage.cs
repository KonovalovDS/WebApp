using System.Security.Cryptography;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class UserStorage {
        private readonly Dictionary<Guid, User> _users = new();

        public UserStorage() {
            var admin = new User {
                Username = "admin",
                Email = "admin@gmail.com",
                HashPassword = "hash",
                isAdmin = true
            };
            _users[admin.Id] = admin;
        }

        public User? GetByUsername(string username) => _users.Values.FirstOrDefault(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        public void AddUser(User user) {
            _users[user.Id] = user;
        }
    }
}
