using Learn.Metrans.PERSISTANCE.Entities;
using Microsoft.EntityFrameworkCore;

namespace Learn.Metrans.PERSISTANCE.Context;

public class MetransDbContext : DbContext
{
    public MetransDbContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<Employees> Employyes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employees>()
            .Property(p => p.Name)
            .HasMaxLength(20);
        modelBuilder.Entity<Employees>()
            .Property(p => p.Surname)
            .HasMaxLength(20);
        modelBuilder.Entity<Employees>()
            .HasKey(p => p.Id);
    }
}
