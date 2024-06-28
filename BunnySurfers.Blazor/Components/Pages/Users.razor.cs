using BunnySurfers.API.DTOs;
using BunnySurfers.Blazor.Services;
using Microsoft.AspNetCore.Components;

namespace BunnySurfers.Blazor.Components.Pages
{
    public partial class Users
    {
        [Inject]
        public IUserService UserService { get; set; } = null!;
        [Inject]
        public NavigationManager navManager { get; set; } = null!;

        public IEnumerable<UserGetDTO>? UserList { get; set; } = null;

        protected bool getUsersError;
        protected bool shouldRender;
        protected string StatusClass = string.Empty;
        protected string StatusMessage = string.Empty;

        private async Task LoadUsers()
        {
            try
            {
                UserList = await UserService.GetAllUsers();
            }
            catch (HttpRequestException)
            {
                getUsersError = true;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadUsers();
            shouldRender = true;
        }

        public async Task DeleteUser(int userId)
        {
            var success = await UserService.DeleteUser(userId);
            if (success)
            {
                StatusClass = "alert-success";
                StatusMessage = "User successfully deleted";
                // Reload the Users to update the table
                await LoadUsers();
            }
            else
            {
                StatusClass = "alert-danger";
                StatusMessage = "Error while deleting user";
            }
        }
    }
}
