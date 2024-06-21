using BunnySurfers.API.Data;
using BunnySurfers.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BunnySurfers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly LMSDbContext _dbContext;

        public CourseController(LMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllCourses()
        {
            var courses = await _dbContext.Courses.ToListAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourseById(int id)
        {
            var course = await _dbContext.Courses.FirstOrDefaultAsync(c => c.CourseId == id);
            if (course == null)
                return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] Course course)
        {
            if (course == null)
            {
                return BadRequest("Invalid course data!");
            }
            _dbContext.Courses.Add(course);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCourseById), new { id = course.CourseId }, course);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course updatedCourse)
        {
            var existingCourse = await _dbContext.Courses.FindAsync(id);
            if (existingCourse == null)
                return NotFound();

            existingCourse.Name = updatedCourse.Name;
            existingCourse.Description = updatedCourse.Description;
            existingCourse.StartDate = updatedCourse.StartDate;
            existingCourse.EndDate = updatedCourse.EndDate;

            await _dbContext.SaveChangesAsync();
            return Ok(existingCourse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var courseToDelete = await _dbContext.Courses.FindAsync(id);
            if (courseToDelete == null)
                return NotFound();

            _dbContext.Courses.Remove(courseToDelete);
            await _dbContext.SaveChangesAsync();
            return NoContent();
            ;
        }
    }
}
