using Microsoft.EntityFrameworkCore;

namespace NewDatabaseProject.Models;

public class GymDbContext : DbContext
{
    public GymDbContext()
    {
        DbPath = "Database/Gym.db";
        Database.EnsureCreated();
    }

    public DbSet<MemberModel> Members { get; set; }
    public DbSet<TrainerModel> Trainers { get; set; }
    public DbSet<ClassModel> Classes { get; set; }
    public DbSet<MembershipTypeModel> MembershipTypes { get; set; }

    public string DbPath { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }
}