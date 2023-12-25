using AuctionService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:7001");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.




app.UseAuthorization();

app.MapControllers();

try
{
    DbInitializer.InitDB(app);
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}

app.Run();
