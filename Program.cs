using Microsoft.EntityFrameworkCore;
using Advantage.API.Models;
using Advantage.APi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApiContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("AdvantageDb"));
});
builder.Services.AddTransient<DataSeed>();


var app = builder.Build();

app.MapControllerRoute(name: "default",
    pattern: "api/{controller}/{action}/{id?}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seed = services.GetRequiredService<DataSeed>();
    seed.SeedData(20, 1000);
}

app.Run();
