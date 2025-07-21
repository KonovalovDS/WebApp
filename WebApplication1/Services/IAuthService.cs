namespace WebApplication1.Services {
    public interface IAuthService {
        bool ValidateUser(string username, string password);
        string GenerateToken(string username, string role);
    }
}
