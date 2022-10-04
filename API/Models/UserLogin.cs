namespace API.Models; 

public class UserLogin {
    public string Username { get; set; } = null!;
    
    private string _password = null!;
    public string Password {
        get => _password;
        set => _password = User.HashPassword(value);
    }
}