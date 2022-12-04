using System.ComponentModel.DataAnnotations.Schema;

namespace ProfitHeal_API.Models.ReportModels;

public class Symptom {
    public string Name { get; init; } = null!;
    public SymptomCategory Category { get; init; } = null!;
}
