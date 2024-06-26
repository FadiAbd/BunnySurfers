using AutoMapper;
using BunnySurfers.API.Data;
using BunnySurfers.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BunnySurfers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(LMSDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly LMSDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        // Get all users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserForGetDTO>>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            var userDTOs = _mapper.Map<IEnumerable<UserForGetDTO>>(users);
            return Ok(userDTOs);
        }

        // Get a single specified user
        [HttpGet("{userId:int}")]
        public async Task<ActionResult<UserForGetDTO>> GetSingleUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user is null)
                return NotFound($"Could not find user with ID {userId}");
            return Ok(_mapper.Map<UserForGetDTO>(user));
        }
    }
}
