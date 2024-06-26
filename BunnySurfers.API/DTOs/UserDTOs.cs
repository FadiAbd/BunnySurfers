using BunnySurfers.API.Entities;

namespace BunnySurfers.API.DTOs
{
    public class UserForPostDTO
    {
        public UserRole Role { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }

    public class UserForGetDTO : UserForPostDTO
    {
        public int UserId { get; set; }
    }
}
