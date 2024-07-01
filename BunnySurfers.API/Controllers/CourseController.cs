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
      

        public CourseController(LMSDbContext dbContext, HttpClient httpClient)
        {
            _dbContext = dbContext;
           
        }

        [HttpGet]
        
        public async Task<ActionResult<List<Course>>> GetAllCourses()
        {
            var courses = await _dbContext.Courses.ToListAsync();
            return Ok(courses);
        }

        [HttpGet("{courseId}")]
        public async Task<ActionResult<Course>> GetCourseById(int courseId)
        {
            var course = await _dbContext.Courses.FirstOrDefaultAsync(c => c.CourseId == courseId);
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
            return CreatedAtAction(nameof(CreateCourse), new { id = course.CourseId }, course);
        }

        [HttpPut("{courseId}")]
        public async Task<IActionResult> UpdateCourse(int courseId, [FromBody] Course updatedCourse)
        {
            var existingCourse = await _dbContext.Courses.FindAsync(courseId);
            if (existingCourse == null)
                return NotFound();

            existingCourse.Name = updatedCourse.Name;
            existingCourse.Description = updatedCourse.Description;
            existingCourse.StartDate = updatedCourse.StartDate;
            existingCourse.EndDate = updatedCourse.EndDate;

            await _dbContext.SaveChangesAsync();
            return Ok(existingCourse);
        }

        [HttpDelete("{courseId}")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            var courseToDelete = await _dbContext.Courses.Include(c => c.Modules)
                                                         .ThenInclude(m => m.Activities)
                                                         .FirstOrDefaultAsync(c => c.CourseId == courseId);  
            if (courseToDelete == null)
                return NotFound();

            _dbContext.Courses.Remove(courseToDelete);
            await _dbContext.SaveChangesAsync();
            return NoContent();
            
        }
    }
}
