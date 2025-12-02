using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace NewDatabaseProject.Models;
public class GymDbContext : DbContext
{
    public DbSet<MemberModel> Members { get; set; }
    public DbSet<TrainerModel> Trainers { get; set; }
    public DbSet<ClassModels> Classes { get; set; }
    public DbSet<MembershipTypeModel> MembershipTypes { get; set; }

    public string DbPath { get; }
    public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "Gym.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}