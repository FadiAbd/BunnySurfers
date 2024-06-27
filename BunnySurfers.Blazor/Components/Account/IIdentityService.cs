namespace BunnySurfers.Blazor.Services
{
    public interface IIdentityService
    {
        Task<bool> IsInRoleAsync(string userId, string roleName);
        // Add other methods as needed
    }
}