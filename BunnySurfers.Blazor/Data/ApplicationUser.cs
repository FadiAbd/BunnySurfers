using BunnySurfers.API.DTOs;
using BunnySurfers.API.Entities;
using Microsoft.AspNetCore.Identity;

namespace BunnySurfers.Blazor.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        // Add key parameters to identify an ApplicationUser with an API User
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public UserRole Role { get; set; }

        // Translating between API and application users
        public static ApplicationUser FromAPIEditUser(UserEditDTO userEditDTO) => new()
        {
            Name = userEditDTO.Name,
            Role = userEditDTO.Role,
            Email = userEditDTO.Email,
            EmailConfirmed = false
        };

        public static ApplicationUser FromAPIGetUser(UserGetDTO userGetDTO) => new()
        {
            UserId = userGetDTO.UserId,
            Name = userGetDTO.Name,
            Role = userGetDTO.Role,
            Email = userGetDTO.Email,
            EmailConfirmed = false
        };

        public UserEditDTO ToUserEditDTO() => new()
        {
            Name = Name,
            Role = Role,
            Email = Email ?? string.Empty
        };

        public UserGetDTO ToUserGetDTO() => new()
        {
            UserId = UserId,
            Name = Name,
            Role = Role,
            Email = Email ?? string.Empty
        };
    }

}
