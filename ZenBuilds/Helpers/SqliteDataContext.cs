namespace ZenBuilds.Helpers;

using Microsoft.EntityFrameworkCore;

public class SqliteDataContext : DataContext
{
    public SqliteDataContext(IConfiguration configuration) : base(configuration) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // connect to sqlite database
        optionsBuilder.UseSqlite(Configuration.GetConnectionString("ZenBuildsSQLite"));
    }
}
