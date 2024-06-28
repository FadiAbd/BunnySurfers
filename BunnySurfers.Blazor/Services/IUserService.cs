using BunnySurfers.API.DTOs;

namespace BunnySurfers.Blazor.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserGetDTO>?> GetAllUsers();
        Task<UserGetDTO?> GetUser(int userId);
        Task<bool> CreateUser(UserEditDTO userDTO);
        Task<bool> UpdateUser(int userId, UserEditDTO userDTO);
        Task<bool> DeleteUser(int userId);
    }
}
