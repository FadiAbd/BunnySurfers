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
    }

}
