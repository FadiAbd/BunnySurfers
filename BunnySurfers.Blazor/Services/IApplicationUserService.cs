using BunnySurfers.API.DTOs;
using BunnySurfers.Blazor.Data;

namespace BunnySurfers.Blazor.Services
{
    public interface IApplicationUserService
    {
        Task<bool> IsAPIUserInAPIDatabase(UserGetDTO userDTO);
        Task<bool> IsAppUserInAppDatabase(ApplicationUser appUser);
        Task<UserGetDTO?> FindAPIUserInAPIDatabase(UserEditDTO userDTO);
        Task<ApplicationUser?> FindAPIUserInAppDatabase(UserEditDTO userDTO);
        Task<UserGetDTO?> FindAppUserInAPIDatabase(ApplicationUser appUser);
        Task AddAppUserToAppDatabase(ApplicationUser appUser);
        Task<ApplicationUser?> AddAPIUserToAppDatabase(UserGetDTO userGetDTO);
        Task<ApplicationUser?> AddAppUserToAPIDatabase(ApplicationUser appUser, bool inAppDatabase);
        Task<ApplicationUser?> CreateNewUser(UserEditDTO userEditDTO);
        Task<IEnumerable<UserGetDTO>> GetAllUsers();
        Task<bool> DeleteUser(int userId);
        Task<ApplicationUser?> GetUser(int userId);
        Task UpdateUser(ApplicationUser appUser);
    }
}