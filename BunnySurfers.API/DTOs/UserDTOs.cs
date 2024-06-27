using BunnySurfers.API.Entities;

namespace BunnySurfers.API.DTOs
{
    public class UserEditDTO
    {
        public UserRole Role { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }

    public class UserGetDTO : UserEditDTO
    {
        public int UserId { get; set; }
    }
}
