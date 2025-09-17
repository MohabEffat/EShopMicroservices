var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();

app.UseApiConfiguration();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}
app.Run();
