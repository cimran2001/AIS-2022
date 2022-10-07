using System.ComponentModel.DataAnnotations.Schema;

namespace ProfitHeal_API.Models.UserModels;

public class Role {
    public string Name { get; init; } = null!;

    public ICollection<User>? Users { get; set; }
}
