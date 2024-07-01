using BunnySurfers.API.DTOs;

namespace BunnySurfers.Blazor.Services
{
    public interface IActivityService
    {
        Task CreateActivity(ActivityEditDTO activity);
    }
}
