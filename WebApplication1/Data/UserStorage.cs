using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class UserStorage {
        private readonly Dictionary<string, User> _users = new();

        public User? FindByUsername(string username) {
            _users.TryGetValue(username, out var user);
            return user;
        }

        public bool ExistsByUsername(string userName) => _users.ContainsKey(userName);

        public void Add(User user) {
            _users[user.Username] = user;
        }

        public List<User> GetAll() => _users.Values.ToList();
    }
}
