using BunnySurfers.API.Entities;

namespace BunnySurfers.Blazor.Services
{
    public interface ICourseService
    {


        Task<List<Course>> GetCourses();
        Task<Course> GetCourseById(int courseId);
        Task CreateCourse(Course course);
        Task UpdateCourse(int courseId, Course courseyew);
        Task DeleteCourse(int courseId);
    }
}
