using Microsoft.EntityFrameworkCore;

namespace NewDatabaseProject.Database;

public class GymDbContext : DbContext
{
    public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<MemberDatabase> Members => Set<MemberDatabase>();
    public DbSet<TrainerDatabase> Trainers => Set<TrainerDatabase>();
    public DbSet<ClassDatabase> Classes => Set<ClassDatabase>();
    public DbSet<MembershipTypeDatabase> MembershipTypes => Set<MembershipTypeDatabase>();
}