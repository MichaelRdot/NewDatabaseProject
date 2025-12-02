using Microsoft.EntityFrameworkCore;

namespace NewDatabaseProject.Database;

public class GymDbContext : DbContext
{
    public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<MemberModel> Members => Set<MemberModel>();
    public DbSet<TrainerModel> Trainers => Set<TrainerModel>();
    public DbSet<ClassModels> Classes => Set<ClassModels>();
    public DbSet<MembershipTypeModel> MembershipTypes => Set<MembershipTypeModel>();
}