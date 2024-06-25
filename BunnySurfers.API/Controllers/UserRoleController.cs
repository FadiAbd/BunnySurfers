using BunnySurfers.API.Data;
using BunnySurfers.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BunnySurfers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController(LMSDbContext context) : ControllerBase
    {
        private readonly LMSDbContext _context = context;

        private static string ValidRoles()
        {
            var roleNames = Enum.GetNames<UserRole>();
            var roleValues = (int[])Enum.GetValuesAsUnderlyingType<UserRole>();
            var numRoles = roleNames.Length;
            string[] roleStrings = new string[numRoles];
            for (int i = 0; i < numRoles; i++)
                roleStrings[i] = $"{roleNames[i]} ({roleValues[i]})";
            return string.Join("; ", roleStrings);
        }

        // Get the role of a given user
        [HttpGet("{userId:int}")]
        public async Task<ActionResult<UserRole>> GetRole(int userId)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.UserId == userId);
            if (user is null)
                return NotFound($"No user found with ID {userId}");
            else
                return Ok(user.Role);
        }

        // Change the role of a given user
        [HttpPut("{userId:int}")]
        public async Task<IActionResult> PutRole(int userId, [FromBody] UserRole role)
        {
            if (!Enum.IsDefined<UserRole>(role))
                return BadRequest(
                    $"The given UserRole {role} was not valid. Valid values are: {ValidRoles()}");

            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.UserId == userId);
            if (user is null)
                return NotFound($"No user found with ID {userId}");

            user.Role = role;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(u => u.UserId == userId))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }
    }
}
