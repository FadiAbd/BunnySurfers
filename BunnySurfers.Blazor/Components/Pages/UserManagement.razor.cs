using System.Net.Http;
using System.Text.Json;
using BunnySurfers.API.DTOs;
using BunnySurfers.API.Entities;
using BunnySurfers.API.Utilities;
using BunnySurfers.Blazor.Services;
using Microsoft.AspNetCore.Components;

namespace BunnySurfers.Blazor.Components.Pages
{
    public partial class UserManagement
    {
        [Inject]
        public HttpClient ApiClient { get; set; } = null!;
        [Inject]
        public IUserService UserService { get; set; } = null!;

        [Parameter]
        public int UserId { get; set; }

        public UserEditDTO? UserDTO { get; set; } = null;

        protected bool getUserError;
        protected bool shouldRender;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                UserDTO = await UserService.GetUser(UserId);
            }
            catch (HttpRequestException)
            {
                getUserError = true;
                shouldRender = true;
                return;
            }
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
