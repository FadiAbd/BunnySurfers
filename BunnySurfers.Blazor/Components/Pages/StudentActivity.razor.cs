using System.Text.Json;
using BunnySurfers.API.DTOs;
using Microsoft.AspNetCore.Components;

namespace BunnySurfers.Blazor.Components.Pages
{
    public partial class StudentActivity
    {
        [Parameter]
        public int StudentId { get; set; }
        [Parameter]
        public int ModuleId { get; set; }
        [Parameter]
        public int ActivityId { get; set; }

        [Inject]
        public HttpClient ApiClient { get; set; } = null!;

        public CourseForStudentViewDTO? Course { get; set; } = null;
        public ModuleForStudentViewDTO? Module { get; set; } = null!;
        public ActivityForStudentViewDTO? Activity { get; set; } = null!;

        private bool getCourseError;
        private bool shouldRender;
        private const string DTFormat = "yyyy-MM-dd HH:mm";

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

            // Parse the API response into a Course and a Module
            using var responseStream = await response.Content.ReadAsStreamAsync();
            Course = await JsonSerializer.DeserializeAsync<CourseForStudentViewDTO?>(responseStream);
            Module = Course?.Modules.FirstOrDefault(m => m.ModuleId == ModuleId);
            Activity = Module?.Activities.FirstOrDefault(a => a.ActivityId == ActivityId);

            shouldRender = true;
        }
    }
}
