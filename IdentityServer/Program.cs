


using IdentityServer.Configurations;

var builder = WebApplication.CreateBuilder(args);

// build IdentityServer to the web application
builder.Services.AddIdentityServer()
    .AddInMemoryApiResources(IdConfig.ApiResources)
    .AddInMemoryApiScopes(IdConfig.ApiScopes)
    .AddInMemoryClients(IdConfig.Clients)
    .AddInMemoryIdentityResources(IdConfig.IdentityResources)
    .AddDeveloperSigningCredential();

var app = builder.Build();

// use identity server
app.UseIdentityServer();



app.Run();