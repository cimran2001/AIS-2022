namespace ProfitHeal_API.Models.UserModels; 

public class UserInfo {
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    
    public DateTime Birthday { get; set; }

    public List<string> Roles { get; set; } = null!;
    
}