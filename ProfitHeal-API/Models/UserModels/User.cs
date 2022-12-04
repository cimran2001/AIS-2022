using ProfitHeal_API.Models.ReportModels;

namespace ProfitHeal_API.Models.UserModels;

public class User {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<Role> Roles { get; set; } = null!;
    public List<Report> Reports { get; set; } = null!;
    public LoginCredentials LoginCredentials { get; set; } = null!;
}
