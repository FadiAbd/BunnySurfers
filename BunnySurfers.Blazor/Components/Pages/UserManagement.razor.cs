using System.Net.Http;
using System.Text.Json;
using BunnySurfers.API.DTOs;
using BunnySurfers.API.Entities;
using BunnySurfers.API.Utilities;
using Microsoft.AspNetCore.Components;

namespace BunnySurfers.Blazor.Components.Pages
{
    public partial class UserManagement
    {
        [Inject]
        public HttpClient ApiClient { get; set; } = null!;

        [Parameter]
        public int UserId { get; set; }

        public UserEditDTO? UserDTO { get; set; } = null;

        protected bool getUserError;
        protected bool shouldRender;

        protected override async Task OnInitializedAsync()
        {
            // Set up and execute the API call
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Users/{UserId}");
            var response = await ApiClient.SendAsync(request);

            // If the API call is unsuccessful, return now
            if (!response.IsSuccessStatusCode)
            {
                getUserError = true;
                shouldRender = true;
                return;
            }

            // Parse the API response into a User
            using var responseStream = await response.Content.ReadAsStreamAsync();
            UserDTO = await JsonSerializer.DeserializeAsync<UserGetDTO?>(responseStream);
            shouldRender = true;
        }

        protected void OnUserRoleChanged(int selectedRoleInt)
        {
            UserDTO!.Role = (UserRole)selectedRoleInt;
        }

        protected async Task OnSubmit()
        {
            // Set up and execute the API call
            var response = await ApiClient.PutAsJsonAsync($"api/Users/{UserId}", UserDTO);
            response.EnsureSuccessStatusCode();
        }
    }
}
