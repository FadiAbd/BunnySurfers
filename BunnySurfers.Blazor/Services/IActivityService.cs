using BunnySurfers.API.DTOs;

namespace BunnySurfers.Blazor.Services
{
    public interface IActivityService
    {
        Task<List<ActivityGetDTO>> GetActivities();
        Task <ActivityGetDTO> GetActivityById(int id);
        Task CreateActivity(ActivityEditDTO activity);
        Task UpdateActivity(int activityId, ActivityEditDTO activity_to_backend);
    }
}
