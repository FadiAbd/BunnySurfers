﻿using BunnySurfers.API.DTOs;
using BunnySurfers.API.Entities;
using BunnySurfers.Blazor.Services;
using Microsoft.AspNetCore.Components;

namespace BunnySurfers.Blazor.Components.Pages
{
    public partial class UserAdd
    {
        [Inject]
        public IUserService UserService { get; set; } = null!;
        [Inject]
        public IApplicationUserService ApplicationUserService { get; set; } = null!;
        [Inject]
        public NavigationManager NavManager { get; set; } = null!;

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
            if (!success)
            {
                StatusClass = "alert-danger";
                StatusMessage = "There was an error adding the user to the database";
                return;
            }

            //await ApplicationUserService.AddUser(UserDTO);
            StatusClass = "alert-success";
            StatusMessage = "User successfully added to the database";
            IsSaved = true;
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
