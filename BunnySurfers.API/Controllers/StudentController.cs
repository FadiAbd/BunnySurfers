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
        [HttpGet]
        public async Task<ActionResult<Course?>> GetCourse(int userId)
        {
            var student = await _context.Users
                .Include(u => u.Courses)
                .ThenInclude(c => c.Users)
                .SingleOrDefaultAsync(u => u.UserId == userId);
            if (student is null)
                return NotFound($"A user with ID {userId} could not be found");
            if (student.Role != UserRole.Student)
                return BadRequest($"The user with ID {userId} is not a student");

            var course = student.Courses.SingleOrDefault();
            return Ok(course);
        }

        // Get the modules for the course the student is in
        [HttpGet("modules")]
        public async Task<ActionResult<IEnumerable<Module>>> GetModules(int userId)
        {
            var student = await _context.Users
                .Include(u => u.Courses)
                .ThenInclude(c => c.Modules)
                .SingleOrDefaultAsync(u => u.UserId == userId);
            if (student is null)
                return NotFound($"A user with ID {userId} could not be found");
            if (student.Role != UserRole.Student)
                return BadRequest($"The user with ID {userId} is not a student");

            var course = student.Courses.SingleOrDefault();
            if (course is null)
                return BadRequest($"The student with ID {userId} is not currently enrolled in a course");

            return Ok(course.Modules);
        }
    }
}
