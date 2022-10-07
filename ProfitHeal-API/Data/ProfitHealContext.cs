using Microsoft.EntityFrameworkCore;
using ProfitHeal_API.Models;
using ProfitHeal_API.Models.ReportModels;
using ProfitHeal_API.Models.UserModels;

namespace ProfitHeal_API.Data; 

public class ProfitHealContext : DbContext {
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<LoginCredentials> LoginCredentials { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    
    public DbSet<Symptom> Symptoms { get; set; } = null!;
    public DbSet<Report> Reports { get; set; } = null!;

    public ProfitHealContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<User>(user => {
            user.HasMany(u => u.Reports)
                .WithOne(r => r.User);
        });

        modelBuilder.Entity<LoginCredentials>(loginCredentials => {
            loginCredentials.HasKey(lc => lc.Username);
            loginCredentials.HasIndex(lc => lc.Username).IsUnique();
        });

        modelBuilder.Entity<Role>(role => {
            role.HasKey(r => r.Name);
            role.HasIndex(r => r.Name).IsUnique();
            
            role.HasData(new Role() { Name = "User" });
            role.HasData(new Role() { Name = "Admin" });
        });

        modelBuilder.Entity<Symptom>(symptom => {
            symptom.HasKey(s => s.Name);
            symptom.HasIndex(s => s.Name).IsUnique();
        });
    }
}
