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
    public DbSet<Like> Likes { get; set; }
    public DbSet<Build> Builds { get; set; }
    public DbSet<UserLog> UserLogs { get; set; }

    /// <summary>
    ///     Define follower composite key    
    ///     Define follower and user relations to resolve problem of two follower-collections in user entity
    ///     DeleteBehavior.Restrict set foreing keys to null when dependent entitie delete
    /// </summary>
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Follower>().HasKey(x => new { x.User_UserId, x.Follower_UserId });
        //builder.Entity<Like>().HasKey(x => x.Id);

        builder.Entity<Follower>()
            .HasOne(x => x.Follower_User)
            .WithMany(x => x.Followers)
            .HasForeignKey(x => x.Follower_UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Follower>()
            .HasOne(x => x.User_User)
            .WithMany(x => x.Following)
            .HasForeignKey(x => x.User_UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<User>()
            .HasMany(x => x.Builds)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.Restrict);





    }

}

