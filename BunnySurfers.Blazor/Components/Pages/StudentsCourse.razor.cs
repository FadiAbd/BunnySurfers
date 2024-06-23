using System.Text.Json;
using BunnySurfers.API.Entities;
using Microsoft.AspNetCore.Components;

namespace BunnySurfers.Blazor.Components.Pages
{
    public partial class StudentsCourse
    {
        [Parameter]
        public int StudentId { get; set; }

        [Inject]
        public IHttpClientFactory ClientFactory { get; set; } = null!;
        [Inject]
        private IConfiguration Configuration { get; set; } = null!;
        private string APIRootUrl { get; set; } = string.Empty;

        public Course? Course { get; set; } = null;
        public IEnumerable<User> Participants { get; set; } = [];

        private bool getCourseError;
        private bool shouldRender;

        protected override bool ShouldRender() =>
            shouldRender;

        protected override async Task OnInitializedAsync()
        {
            // Get the root URL for API calls from the configuration (appsettings.json)
            APIRootUrl = Configuration.GetValue<string>("APIRootUrl")
                ?? throw new Exception("Need to configure 'APIRootUrl' in appsettings.json");

            // Set up and execute the API call
            var request = new HttpRequestMessage(HttpMethod.Get, $"{APIRootUrl}/api/Student/{StudentId}");
            var client = ClientFactory.CreateClient();
            var response = await client.SendAsync(request);

            // Parse the response from the API
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                Course = await JsonSerializer.DeserializeAsync<Course?>(responseStream);
            }
            else
            {
                getCourseError = true;
            }

            shouldRender = true;
        }
    }
}
