namespace Profit; 

public class Role {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    
    public virtual IEnumerable<User>? Users { get; set; }
}
