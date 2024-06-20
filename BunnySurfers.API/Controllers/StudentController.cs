using BunnySurfers.API.Data;
using BunnySurfers.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BunnySurfers.API.Controllers
{
    [Route("api/[controller]/{userId:int}")]
    [ApiController]
    public class StudentController(LMSDbContext context) : ControllerBase
    {
        private readonly LMSDbContext _context = context;

        // Get the course this student is in, if any
        [HttpGet("course")]
        public async Task<ActionResult<Course?>> GetCourse(int userId)
        {
            var student = await _context.Users
                .Include(u => u.Courses)
                .SingleOrDefaultAsync(u => u.UserId == userId);
            if (student is null)
                return NotFound();
            if (student.Role != UserRole.Student)
                return BadRequest($"The user with ID {userId} is not a student");

            var course = student.Courses.SingleOrDefault();
            return Ok(course);
        }

        // Get the participants of the student's course
        [HttpGet("course/participants")]
        public async Task<ActionResult<IEnumerable<User>>> GetParticipants(int userId)
        {
            var student = await _context.Users
                .Include(u => u.Courses)
                .ThenInclude(c => c.Users)
                .SingleOrDefaultAsync(u => u.UserId == userId);
            if (student is null)
                return NotFound();
            if (student.Role != UserRole.Student)
                return BadRequest($"The user with ID {userId} is not a student");

            var course = student.Courses.SingleOrDefault();
            var participants = (course is null) ? Enumerable.Empty<User>() : course.Users;
            return Ok(participants);
        }
    }
}
