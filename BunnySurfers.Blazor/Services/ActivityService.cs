using BunnySurfers.API.Entities;

namespace BunnySurfers.Blazor.Services
{
    public class ActivityService : IActivityService
    {
        private readonly HttpClient _httpClient;

        public ActivityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CreateActivity(Activity activity)
        {
             var response = await _httpClient.PostAsJsonAsync($"api/Activity", activity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteActivity(int activityId)
        {
            var response = await _httpClient.DeleteAsync($"api/Activity/{activityId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<Activity>> GetActivities()
        {
            return await _httpClient.GetFromJsonAsync<List<Activity>>("api/Activity");
        }

        public async Task<Activity> GetActivityById(int activityId)
        {
            return await _httpClient.GetFromJsonAsync<Activity>($"api/Activity/{activityId}");
        }

        public async Task UpdateActivity(int activityId, Activity activityyew)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Activity/ {activityId}", activityyew);
            response.EnsureSuccessStatusCode();
        }
    }
}
