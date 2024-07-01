using AutoMapper;
using BunnySurfers.API.Data;
using BunnySurfers.API.DTOs;
using BunnySurfers.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BunnySurfers.API.Controllers
{
    [Route("api/[controller]/{studentId:int}")]
    [ApiController]
    public class StudentViewController(LMSDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly LMSDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        // Get the course this student is in, if any
        [HttpGet]
        public async Task<ActionResult<CourseForStudentViewDTO?>> GetCourse(int studentId)
        {
            var student = await _context.Users
                .Include(u => u.Documents)
                .Include(u => u.Courses)
                .ThenInclude(c => c.Users)
                .Include(u => u.Courses)
                .ThenInclude(c => c.Documents)
                .Include(u => u.Courses)
                .ThenInclude(c => c.Modules)
                .ThenInclude(m => m.Documents)
                .Include(u => u.Courses)
                .ThenInclude(c => c.Modules)
                .ThenInclude(m => m.Activities)
                .ThenInclude(a => a.Documents)
                .AsSplitQuery()
                .FirstOrDefaultAsync(u => u.UserId == studentId);

            if (student is null)
                return NotFound($"A user with ID {studentId} could not be found");
            if (student.Role != UserRole.Student)
                return BadRequest($"The user with ID {studentId} is not a student");

            var course = student.Courses.FirstOrDefault();
            var courseDTO = (course is null) ? null : _mapper.Map<CourseGetDTO>(course);
            return Ok(courseDTO);
        }
    }
}
