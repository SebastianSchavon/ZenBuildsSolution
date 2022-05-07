namespace ZenBuilds.Helpers;

using Microsoft.EntityFrameworkCore;
using ZenBuilds.Entities;

public class DataContext : DbContext
{
    // get connectionstring from appsettings.json
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ZenBuildsDataBase"));
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Follower> Followers { get; set; }
    public DbSet<Build> Builds { get; set; }
    public DbSet<UserLog> UserLogs { get; set; }


    // fluent api to create composite key with user and follower IDs
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // composite primary key 
        builder.Entity<Follower>().HasKey(x => new { x.User_UserId, x.Follower_UserId });
        builder.Entity<Build>().HasKey(x => new { x.UserId, x.Id });

        //// setting follower relations to resolve problem of not having two follower-collections in user entity
        //builder.Entity<Follower>()
        //    .HasOne(x => x.Follower_User)
        //    .WithMany()
        //    .HasForeignKey(x => x.Follower_UserId)
        //    .OnDelete(DeleteBehavior.Restrict);

        //builder.Entity<Follower>()
        //    .HasOne(x => x.User_User)
        //    .WithMany(x => x.Followers)
        //    .HasForeignKey(x => x.User_UserId);

        // setting follower relations to resolve problem of not having two follower-collections in user entity
        builder.Entity<Follower>()
            .HasOne(x => x.Follower_User)
            .WithMany(x => x.Followers)
            .HasForeignKey(x => x.Follower_UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Follower>()
            .HasOne(x => x.User_User)
            .WithMany(x => x.Following)
            .HasForeignKey(x => x.User_UserId);
    }

}

