using BunnySurfers.API.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BunnySurfers.Blazor.Services
{
    public class CourseService : ICourseService
    {
        private readonly HttpClient _httpClient;

        public CourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Course>> GetCourses()
        {
            return await _httpClient.GetFromJsonAsync<List<Course>>("api/Course");
        }

        public async Task<Course> GetCourseById(int courseId)
        {
            return await _httpClient.GetFromJsonAsync<Course>($"api/Course/{courseId}");
        }

        public async Task CreateCourse(Course course)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Course", course);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateCourse(int courseId, Course courseyew)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Course/{courseId}", courseyew);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCourse(int courseId)
        {
            var response = await _httpClient.DeleteAsync($"api/Course/{courseId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
