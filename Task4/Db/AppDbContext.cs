using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task4.Db.Entities;
using Task4.Db.Mapping;

namespace Task4.Db
{
    public class AppDbContext : IdentityDbContext<UserEntity, RoleEntity, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(builder);

            builder.Entity<UserEntity>().ToTable("Users");
            builder.Entity<RoleEntity>().ToTable("Roles");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");

            builder.Entity<RoleEntity>().HasData(new[]
            {
                new RoleEntity { Id = 1, Name = "user", NormalizedName = "USER" },
                new RoleEntity { Id = 2, Name = "admin", NormalizedName = "ADMIN" }
            });

            var userName = "admin@gmail.com";
            var password = "abc123";

            var admin = new UserEntity
            {
                Id = 1,
                UserName = userName,
                NormalizedEmail = userName.ToUpper(),
                NormalizedUserName = userName.ToUpper(),
            };

            var hasher = new PasswordHasher<UserEntity>();
            admin.PasswordHash = hasher.HashPassword(admin, password);
            builder.Entity<UserEntity>().HasData(admin);

            builder.Entity<IdentityUserRole<int>>().HasData(new[]
            {
                new IdentityUserRole<int> { UserId = 1, RoleId = 2 }
            });
        }
    }
}
