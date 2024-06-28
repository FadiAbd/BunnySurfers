using BunnySurfers.API.DTOs;

namespace BunnySurfers.Blazor.Services
{
    public interface IApplicationUserService
    {
        Task AddUser(UserEditDTO userDTO);
    }
}