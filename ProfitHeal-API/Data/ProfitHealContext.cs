using Microsoft.EntityFrameworkCore;

namespace Profit; 

public class ProfitHealContext : DbContext {
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<LoginCredentials> LoginCredentials { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;

    public ProfitHealContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<User>(user => {
            user.HasMany(u => u.Roles)
                .WithMany(r => r.Users);
        });
    }
}