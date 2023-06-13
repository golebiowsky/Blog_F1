using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog_F1.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = "914074e1-2381-43be-8094-df8dcffb8049";
            var superAdminRoleId = "1864ebc2-3044-4776-8904-8354366078de";
            var userRoleId = "0e2b41eb-9ce3-49b1-99fa-0e071f980dd2";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name="Admin",
                    NormalizedName="Admin",
                    Id=adminRoleId,
                    ConcurrencyStamp=adminRoleId
                },
                new IdentityRole
                {
                    Name="SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id=superAdminRoleId,
                    ConcurrencyStamp=superAdminRoleId
                },
                new IdentityRole
                {
                    Name="User",
                    NormalizedName="User",
                    Id=userRoleId,
                    ConcurrencyStamp=userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            var superAdminId = "0e046456-e88a-4355-a1b2-741d526f849c";

            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@blogf1.com",
                Email = "superadmin@blogf1.com",
                NormalizedEmail = "superadmin@blogf1.com".ToUpper(),
                NormalizedUserName = "superadmin@blogf1.com".ToUpper(),
                Id = superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser,"Superadmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId= adminRoleId,
                    UserId= superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId= superAdminRoleId,
                    UserId= superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId= userRoleId,
                    UserId= superAdminId
                }

            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

        }
    }
}
