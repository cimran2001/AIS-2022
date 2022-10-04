using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data; 

public class AISContext : DbContext {
    public DbSet<User> Users { get; set; } = null!;

    public AISContext(DbContextOptions options) : base(options) {}
}