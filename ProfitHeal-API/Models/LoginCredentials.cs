using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Profit; 

public class LoginCredentials {
    public int Id { get; set; }
    public string Username { get; set;  } = null!;
    
    private string _password = null!;

    public string Password {
        get => _password;
        set => _password = HashPassword(value);
    }

    private static string HashPassword(string password) {
        var data = Encoding.UTF8.GetBytes(password);
        using var algorithm = SHA512.Create();
        var hash = algorithm.ComputeHash(data);
        return Convert.ToBase64String(hash);
    }

    public override bool Equals(object? obj) => this.Equals(obj as LoginCredentials);

    public static bool operator ==(LoginCredentials obj1, LoginCredentials obj2)
    {
        return Equals(obj1, obj2);
    }

    public static bool operator !=(LoginCredentials obj1, LoginCredentials obj2) {
        return !(obj1 == obj2);
    }

    private bool Equals(LoginCredentials? other) {
        if (other is null)
            return false;
        return Password == other.Password && Username == other.Username;
    }

    public override int GetHashCode() {
        return HashCode.Combine(Username, Password);
    }
}
