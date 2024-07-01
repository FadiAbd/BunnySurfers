using BunnySurfers.API.DTOs;
using BunnySurfers.API.Entities;
using BunnySurfers.Blazor.Services;
using Microsoft.AspNetCore.Components;

namespace BunnySurfers.Blazor.Components.Pages
{
    public partial class UserEdit
    {
        [Inject]
        public IUserService UserService { get; set; } = null!;

        [Parameter]
        public int UserId { get; set; }

        public UserEditDTO? UserDTO { get; set; } = null;

        protected bool getUserError;
        protected bool shouldRender;
        protected bool putUserError;
        protected bool IsSaved;
        protected string StatusClass = string.Empty;
        protected string StatusMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                UserDTO = await UserService.GetUser(UserId);
            }
            catch (HttpRequestException)
            {
                getUserError = true;
            }
            shouldRender = true;
        }

        protected void OnUserRoleChanged(int selectedRoleInt)
        {
            UserDTO!.Role = (UserRole)selectedRoleInt;
        }

        protected async Task OnSubmit()
        {
            try
            {
                await UserService.UpdateUser(UserId, UserDTO!);
            }
            catch (HttpRequestException)
            {
                putUserError = true;
                StatusClass = "alert-danger";
                StatusMessage = "An error occurred while updating the user";
                return;
            }

            StatusClass = "alert-success";
            StatusMessage = "User successfully updated";
            IsSaved = true;
        }
    }
}
