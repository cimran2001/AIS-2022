using ProfitHeal_API.Models.UserModels;

namespace ProfitHeal_API.Models.ReportModels;

public class Report {
    public int Id { get; set; }
    public DateTime Date { get; set; }
    
    public Symptom[] Symptoms { get; set; } = null!;
    
    public User User { get; set; } = null!;
}
