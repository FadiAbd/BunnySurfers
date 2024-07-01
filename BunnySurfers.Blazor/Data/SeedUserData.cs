using BunnySurfers.API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BunnySurfers.Blazor.Services;

namespace BunnySurfers.Blazor.Data
{
    public class SeedUserData(ApplicationDbContext context, IUserService userService)
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IUserService _userService = userService;
        private readonly ApplicationUserService _appService = new(context, userService);

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
        
        public async Task SeedAdminUser()
        {
            // Create an ApplicationUser with all fields except UserId
            var appUser = new ApplicationUser
            {
                Name = "Admin McAdmin",
                Email = "admin@admin.com",
                Role = UserRole.Admin,
                NormalizedEmail = "ADMIN@ADMIN.COM",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                EmailConfirmed = true,
            };
            if (appUser is null)
                return;
            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(appUser, "asd,./123");
            appUser.PasswordHash = hashed;

            if (!await _appService.IsAppUserInAppDatabase(appUser))
            {
                // First add the ApplicationUser to the API database
                appUser = await _appService.AddAppUserToAPIDatabase(appUser, inAppDatabase: false);
                if (appUser is null)
                    return;
                // Then add this appUser (with UserId) to the app database
                await _appService.AddAppUserToAppDatabase(appUser);
            }
        }

        public async Task SeedAppDatabaseFromAPIDatabase()
        {
            // Now copy over all the users from the API
            var userGetDTOs = await _userService.GetAllUsers();
            if (userGetDTOs is null)
                return;
            foreach (var userGetDTO in userGetDTOs)
                await _appService.AddAPIUserToAppDatabase(userGetDTO);
        }

        public async Task SeedAppDatabase()
        {
            //_context.Database.EnsureDeleted();
            await _context.Database.EnsureCreatedAsync();
            await SeedUserRoles();
            await SeedAdminUser();
            await SeedAppDatabaseFromAPIDatabase();
        }
    }
}
