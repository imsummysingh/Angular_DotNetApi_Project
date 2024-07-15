using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "eb65716c-bc53-4d1a-ba68-2280c49dcbee";
            var writerRoleId = "3fb679e4-9760-40cd-b255-b465b6160f67";

            //create reader & writer role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id=writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper(),
                    ConcurrencyStamp=writerRoleId
                }
            };


            //seed the role
            builder.Entity<IdentityRole>().HasData(roles);


            //create an admin user ->using TypeIdentityUser
            var adminUserId = "81b2ef28-b1e8-4214-bbfa-933f92a81ea9";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com",
                NormalizedEmail = "admin@codepulse.com".ToUpper(),
                NormalizedUserName = "admin@codepulse.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");
            
            //seed admin
            builder.Entity<IdentityUser>().HasData(admin);


            //give roles to admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId,
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId,
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

        }
    }
}
