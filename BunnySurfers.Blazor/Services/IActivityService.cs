using BunnySurfers.API.Entities;

namespace BunnySurfers.Blazor.Services
{
    public interface IActivityService
    {
        Task<List<Activity>> GetActivities();
        Task<Activity> GetActivityById(int activityId);
        Task CreateActivity(Activity activity);
        Task UpdateActivity(int activityId, Activity activityyew);
        Task DeleteActivity(int activityId);
    }
}
