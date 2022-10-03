using System.Security.Cryptography;
using System.Text;

namespace API.Models; 

public class User {
    public string Username { get; init; }

    private string _password;

    public string Password {
        get => _password;
        set => _password = HashPassword(value);
    }

    public User(string username, string password) {
        Username = username;
        Password = password;
    }
    
    private string HashPassword(string password) {
        var algorithm = new SHA512CryptoServiceProvider();
        Byte[] inputBytes = Encoding.UTF8.GetBytes(password);
        Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
        return BitConverter.ToString(hashedBytes);
    }
}
