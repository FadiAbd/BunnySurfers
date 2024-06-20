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

        // Check that a user exists and is a student
        private bool CheckStudentExists(int userId)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserId == userId);
            return (user is not null && user.Role == UserRole.Student);
        }

        // Get the course this student is in, if any
        [HttpGet("course")]
        public async Task<ActionResult<Course?>> GetCourse(int userId)
        {
            if (!CheckStudentExists(userId))
                return BadRequest();

            var _student = await _context.Users.Include(x => x.Courses).FirstOrDefaultAsync(x => x.UserId == userId);
            if (_student is null)
                return BadRequest();
            User student = _student;

            var course = student.Courses.SingleOrDefault();
            return Ok(course);
        }
    }
}
