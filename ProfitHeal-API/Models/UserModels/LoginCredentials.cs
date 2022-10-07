using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ProfitHeal_API.Models.UserModels; 

public record LoginCredentials {
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
}
