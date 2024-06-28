using System.Text.Json;
using BunnySurfers.API.DTOs;
using Microsoft.AspNetCore.WebUtilities;

namespace BunnySurfers.Blazor.Services
{
    public class UserService(HttpClient apiClient) : IUserService
    {
        private readonly HttpClient ApiClient = apiClient;

        public async Task<IEnumerable<UserGetDTO>?> GetAllUsers() =>
            await ApiClient.GetFromJsonAsync<IEnumerable<UserGetDTO>>("api/users");

        public async Task<UserGetDTO?> GetUser(int userId) =>
            await ApiClient.GetFromJsonAsync<UserGetDTO?>($"api/users/{userId}");

        public async Task<UserGetDTO?> CreateUser(UserEditDTO userEditDTO)
        {
            var response = await ApiClient.PostAsJsonAsync("api/users", userEditDTO);
            if (!response.IsSuccessStatusCode)
                return null;

            using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<UserGetDTO?>(stream);
        }

        public async Task<bool> UpdateUser(int userId, UserEditDTO userDTO)
        {
            var response = await ApiClient.PutAsJsonAsync($"api/users/{userId}", userDTO);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var response = await ApiClient.DeleteAsync($"api/users/{userId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<UserGetDTO?> FindUserByEmail(string email) =>
            await ApiClient.GetFromJsonAsync<UserGetDTO?>($"api/users/find?email={email}");
    }
}
