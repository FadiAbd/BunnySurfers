using BunnySurfers.API.DTOs;
using System.Net.Http;

namespace BunnySurfers.Blazor.Services
{
    public class ActivityService : IActivityService
    {

        private readonly HttpClient _httpClient;

        public ActivityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ActivityGetDTO>> GetActivities()
        {
            return await _httpClient.GetFromJsonAsync<List<ActivityGetDTO>>("api/Activities");
        }
        public async Task CreateActivity(ActivityEditDTO activity)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Activities", activity);
            response.EnsureSuccessStatusCode();
        }

    }
}
