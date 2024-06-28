using BunnySurfers.API.DTOs;
using BunnySurfers.API.Entities;
using BunnySurfers.Blazor.Services;
using Microsoft.AspNetCore.Components;

namespace BunnySurfers.Blazor.Components.Pages
{
    public partial class UserAdd
    {
        [Inject]
        public IUserService UserService { get; set; } = null!;

        [SupplyParameterFromForm]
        public UserEditDTO UserDTO { get; set; } = null!;

        protected string StatusClass = string.Empty;
        protected string StatusMessage = string.Empty;
        protected bool IsSaved;

        protected override void OnInitialized()
        {
            UserDTO ??= new();
        }

        public async Task HandleValidSubmit()
        {
            var success = await UserService.CreateUser(UserDTO);
            if (success)
            {
                StatusClass = "alert-success";
                StatusMessage = "Employee updated successfully";
                IsSaved = true;
            }
            else
            {
                StatusClass = "alert-danger";
                StatusMessage = "There was an error adding the employee to the database";
            }
        }

        public void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            StatusMessage = "There are validation errors. Please try again.";
        }

        public void OnUserRoleChanged(int selectedRoleInt)
        {
            UserDTO.Role = (UserRole)selectedRoleInt;
        }
    }
}
