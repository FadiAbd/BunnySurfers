using System.Text.Json;
using BunnySurfers.API.DTOs;
using BunnySurfers.API.Entities;
using Microsoft.AspNetCore.Components;

namespace BunnySurfers.Blazor.Components.Pages
{
    public partial class StudentView
    {
        [Parameter]
        public int StudentId { get; set; }

        [Inject]
        private HttpClient ApiClient { get; set; } = null!;

        public CourseForStudentViewDTO? Course { get; set; } = null;
        public UserForStudentViewDTO? Teacher { get; set; } = null;
        public IEnumerable<UserForStudentViewDTO> Students { get; set; } = [];

        private bool getCourseError;
        private bool shouldRender;

        protected override bool ShouldRender() =>
            shouldRender;

        protected override async Task OnInitializedAsync()
        {
            // Set up and execute the API call
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/StudentView/{StudentId}");
            var response = await ApiClient.SendAsync(request);

            // If the API call is unsuccessful, return now
            if (!response.IsSuccessStatusCode)
            {
                getCourseError = true;
                shouldRender = true;
                return;
            }

            // Parse the API response into a Course with participants
            using var responseStream = await response.Content.ReadAsStreamAsync();
            Course = await JsonSerializer.DeserializeAsync<CourseForStudentViewDTO?>(responseStream);
            //var responseString = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(responseString);
            if (Course is not null)
            {
                Teacher = Course.Users.FirstOrDefault(u => u.Role == UserRole.Teacher);
                Students = Course.Users.Where(u => u.Role == UserRole.Student && u.UserId != StudentId).ToList();
            }

            shouldRender = true;
        }
    }
}
