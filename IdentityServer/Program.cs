using IdentityServer.Data;
using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//Configuration

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddIdentity<CustomIdentityUser, IdentityRole<int>>(
        opt => opt.SignIn.RequireConfirmedEmail = true
    )
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

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
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = false;
});
//Scoped

builder.Services.AddScoped<RegisterService, RegisterService>();
builder.Services.AddScoped<LoginService,LoginService>();
builder.Services.AddScoped<TokenService, TokenService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Host.ConfigureAppConfiguration((context, builder) => builder.AddUserSecrets<Program>());

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
