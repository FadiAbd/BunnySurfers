using BunnySurfers.API.DTOs;
using BunnySurfers.Blazor.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BunnySurfers.Blazor.Services
{
    public class ApplicationUserService(ApplicationDbContext context, IUserService userService) : IApplicationUserService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IUserService _userService = userService;

        public async Task<bool> IsAppUserInAppDatabase(ApplicationUser appUser) =>
            await _context.Users.AnyAsync(u => u.Email == appUser.Email);

        public async Task<bool> IsAPIUserInAPIDatabase(UserGetDTO userDTO) =>
            await _userService.GetUser(userDTO.UserId) is not null;

        public async Task<ApplicationUser?> FindAPIUserInAppDatabase(UserEditDTO userDTO) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);

        public async Task<UserGetDTO?> FindAppUserInAPIDatabase(ApplicationUser appUser) =>
            appUser.Email is null ? null : await _userService.FindUserByEmail(appUser.Email);

        public async Task<UserGetDTO?> FindAPIUserInAPIDatabase(UserEditDTO userDTO) =>
            await _userService.FindUserByEmail(userDTO.Email);

        public async Task AddAppUserToAppDatabase(ApplicationUser appUser)
        {
            if (!await IsAppUserInAppDatabase(appUser))
            {
                var userStore = new UserStore<ApplicationUser>(_context);
                await userStore.CreateAsync(appUser);
                await userStore.AddToRoleAsync(appUser, appUser.Role.ToString().ToUpperInvariant());
            }
        }

        public async Task<ApplicationUser?> AddAPIUserToAppDatabase(UserGetDTO userGetDTO)
        {
            var appUser = ApplicationUser.FromAPIGetUser(userGetDTO);
            if (appUser is null)
                return null;

            await AddAppUserToAppDatabase(appUser);
            return appUser;
        }

        public async Task<ApplicationUser?> AddAppUserToAPIDatabase(ApplicationUser appUser, bool inAppDatabase = true)
        {
            // Find or create user in API database
            var userGetDTO = await FindAppUserInAPIDatabase(appUser);
            if (userGetDTO is null)
            {
                var userEditDTO = appUser.ToUserEditDTO();
                userGetDTO = await _userService.CreateUser(userEditDTO);
                if (userGetDTO is null)
                    return null;
            }

            // Update the value in the app database
            if (inAppDatabase)
            {
                var userStore = new UserStore<ApplicationUser>(_context);
                await userStore.UpdateAsync(appUser);
            }
            return appUser;
        }

        public async Task<ApplicationUser?> CreateNewUser(UserEditDTO userEditDTO)
        {
            var userGetDTO = await _userService.CreateUser(userEditDTO);
            if (userGetDTO is null)
                return null;
            return await AddAPIUserToAppDatabase(userGetDTO);
        }
    }
}
