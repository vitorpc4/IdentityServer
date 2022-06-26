using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data
{
    public class AppDbContext : IdentityDbContext<CustomIdentityUser, IdentityRole<int>, int>
    {
        private IConfiguration _configuration;
        public AppDbContext(DbContextOptions<AppDbContext> opt, IConfiguration Configuration) : base(opt)
        {
            _configuration = Configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            CustomIdentityUser admin = new CustomIdentityUser
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                EmailConfirmed = true,
                Id = 99999
            };

            PasswordHasher<CustomIdentityUser> hasher = new PasswordHasher<CustomIdentityUser>();
            admin.PasswordHash = hasher.HashPassword(admin, _configuration.GetValue<string>("AdminInfo:Password"));

            builder.Entity<CustomIdentityUser>().HasData(admin);

            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 99999, Name = "admin", NormalizedName = "ADMIN" }
            );
            builder.Entity <IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 99997, Name = "regular", NormalizedName = "REGULAR"}
            );

            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { RoleId = 99999, UserId = 99999}
                );
        }
    }
}
