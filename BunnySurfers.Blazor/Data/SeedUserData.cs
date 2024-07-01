using BunnySurfers.API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BunnySurfers.Blazor.Data
{
    public class SeedUserData(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public void ClearUserDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        public async Task SeedUserRoles()
        {
            foreach (var role in Enum.GetValues<UserRole>().Cast<UserRole>())
            {
                var roleStore = new RoleStore<IdentityRole>(_context);
                if (!_context.Roles.Any(r => r.Name == role.ToString()))
                    await roleStore.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),
                        NormalizedName = role.ToString().ToUpperInvariant()
                    });
            }
            await _context.SaveChangesAsync();
        }
        
        public async Task SeedUserDatabase()
        {
            var user = new ApplicationUser
            {
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                EmailConfirmed = true,
            };

            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "asd,./123");
                user.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(_context);
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, UserRole.Admin.ToString().ToUpperInvariant());
            }

            await _context.SaveChangesAsync();
        }
    }
}
