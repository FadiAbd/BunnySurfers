using BunnySurfers.API.Entities;
using System.ComponentModel.DataAnnotations;

namespace BunnySurfers.API.DTOs
{
    public class UserEditDTO
    {
        public UserRole Role { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name is too long")]
        [MinLength(1, ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }

    public class UserGetDTO : UserEditDTO
    {
        public int UserId { get; set; }
    }
}
