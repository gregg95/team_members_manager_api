using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Presistence;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {
    }

    public DbSet<TeamMember> TeamMembers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Entity<TeamMember>().HasData(GetTeamMemebers());

        base.OnModelCreating(modelBuilder);
    }

    private List<TeamMember> GetTeamMemebers()
    {
        return new List<TeamMember>() { 
            TeamMember.Create(
                "Marshall Simmons",
                "marshall.simmons@example.com",
                "(309) 822-2653",
                new DateTime(2024, 1, 1)),
            TeamMember.Create(
                "Victoria West",
                "victoria.west@example.com",
                "(603) 232-6206",
                new DateTime(2024, 1, 1)),
        };
    }
}
