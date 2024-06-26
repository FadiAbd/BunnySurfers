using BunnySurfers.API.Entities;

namespace BunnySurfers.API.DTOs
{
    public class UserForGetDTO
    {
        public int UserId { get; set; }
        public UserRole Role { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}
