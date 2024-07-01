using BunnySurfers.API.DTOs;

namespace BunnySurfers.Blazor.Services
{
    public interface IActivityService
    {
        Task<List<ActivityGetDTO>> GetActivities();
        Task CreateActivity(ActivityEditDTO activity);
    }
}
