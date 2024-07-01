using BunnySurfers.API.DTOs;
using BunnySurfers.Blazor.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BunnySurfers.Blazor.Services
{
    public class ApplicationUserService(ApplicationDbContext context) : IApplicationUserService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddUser(UserEditDTO userDTO)
        {
            ApplicationUser appUser = new()
            {
                Email = userDTO.Email,
                NormalizedEmail = userDTO.Email.ToUpperInvariant(),
                EmailConfirmed = false
            };

            if (!_context.Users.Any(u => u.UserName == appUser.UserName))
            {
                var userStore = new UserStore<ApplicationUser>(_context);
                await userStore.CreateAsync(appUser);
                await userStore.AddToRoleAsync(appUser, userDTO.Role.ToString().ToUpperInvariant());
            }
        }
    }
}
