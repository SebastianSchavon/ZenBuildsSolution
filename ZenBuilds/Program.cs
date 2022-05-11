using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using ZenBuilds.Authorization;
using ZenBuilds.Helpers;
using ZenBuilds.Services;

var builder = WebApplication.CreateBuilder(args);

var CustomCorsPolicy = "_customCorsPolicy";

{
    var services = builder.Services;

    services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ZenBuildsDataBase")));

    // limit app access to specific origin
    services.AddCors(options =>
    {
        options.AddPolicy(name: CustomCorsPolicy,
            policy =>
            {
                //policy.WithOrigins("http://localhost:3000")
                policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
    });

    services.AddControllers();

    // enables enum converting
    services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        opts.JsonSerializerOptions.IgnoreNullValues = true;
    });

    // configure automapper with all automapper profiles from this assembly
    services.AddAutoMapper(typeof(Program));

    // configure strongly typed settings object <= pass appsettings value from appsettings.json to AppSettings object. 
    // but why need? cant i access the secret key without it being defined here?
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI (dependancy injections) for application services
    services.AddScoped<IJwtUtils, JwtUtils>();

    services.AddScoped<IBuildService, BuildService>();
    services.AddScoped<IFollowerService, FollowerService>();
    services.AddScoped<IUserLogService, UserLogService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<ILikeService, LikeService>();


}

var app = builder.Build();

// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dataContext.Database.Migrate();
}

{
    app.UseCors(CustomCorsPolicy);

    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}

app.Run("http://localhost:4000");