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

            ////Customizing Table Names
            builder.Entity<IdentityUser>().ToTable("Users");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityRole>().ToTable("Roles");

            CustomIdentityUser admin = new CustomIdentityUser
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Id = 99999
            };

            PasswordHasher<CustomIdentityUser> hasher = new PasswordHasher<CustomIdentityUser>();
            admin.PasswordHash = hasher.HashPassword(admin, _configuration.GetValue<string>("AdminInfo:Password"));

            builder.Entity<CustomIdentityUser>().HasData(admin);

            builder.Entity<IdentityRole<int>>().ToTable("Roles").HasData(
                new IdentityRole<int> { Id = 99999, Name = "admin", NormalizedName = "ADMIN" }
            );

            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles").HasData(
                new IdentityUserRole<int> { RoleId = 99999, UserId = 99999}
                );

          
        }
    }
}
