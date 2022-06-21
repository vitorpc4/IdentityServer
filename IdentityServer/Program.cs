using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configuration

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

//Configurações pomelo
var connectionString = configuration.GetConnectionString("IdentityConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(connectionString,serverVersion)
    );

//Adicionando custom Identity
builder.Services.AddIdentity<CustomIdentityUser, IdentityRole<int>>(
        opt => opt.SignIn.RequireConfirmedEmail = true
    )
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
