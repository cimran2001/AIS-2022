using System.Security.Cryptography;
using System.Text;

namespace API.Models; 

public class User {
    public int Id { get; set; }
    public string Username { get; init; }

    private string _password = null!;

    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public int Age { get; set; }
    public string Email { get; set; } = null!;
    public List<Role> Roles { get; set; } = null!;

    public string Password {
        get => _password;
        set => _password = HashPassword(value);
    }

    public User(string username, string password) {
        Username = username;
        Password = password;
    }

    internal static string HashPassword(string password) {
        var data = Encoding.UTF8.GetBytes(password);
        using var algorithm = SHA512.Create();
        var hash = algorithm.ComputeHash(data);
        return Convert.ToBase64String(hash);
    }
}
