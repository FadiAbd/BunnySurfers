using BunnySurfers.API.DTOs;
using BunnySurfers.Blazor.Components;
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

        public async Task<ActivityGetDTO> GetActivityById(int id)
        {
            return await _httpClient.GetFromJsonAsync<ActivityGetDTO>($"api/Activities/{id}");
        }

        public async Task UpdateActivity(int activityId, ActivityEditDTO activity_to_backend)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Activities/{activityId}", activity_to_backend);
            response.EnsureSuccessStatusCode();
        }
    }
}
