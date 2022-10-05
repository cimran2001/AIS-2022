using System.Security.Cryptography;
using System.Text;

namespace Profit; 

public class User {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public DateTime Birthday { get; set; }
    public string Email { get; set; } = null!;
    public virtual IEnumerable<Role> Roles { get; set; } = null!;

    public int LoginCredentialsId { get; set; }
    public LoginCredentials LoginCredentials { get; set; } = null!;
}
