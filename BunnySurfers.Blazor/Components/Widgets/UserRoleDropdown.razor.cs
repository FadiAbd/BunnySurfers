using BunnySurfers.API.Entities;
using BunnySurfers.API.Utilities;
using Microsoft.AspNetCore.Components;

namespace BunnySurfers.Blazor.Components.Widgets
{
    public partial class UserRoleDropdown
    {
        [Parameter]
        public UserRole? InitialRole { get; set; } = null;

        [Parameter]
        public EventCallback<int> OnUserRoleChanged { get; set; }

        //private int selectedRoleInt;
        //public int SelectedUserRoleInt
        //{
        //    get => selectedRoleInt;
        //    set
        //    {
        //        selectedRoleInt = value;
        //        OnUserRoleChanged.InvokeAsync(value);
        //    }
        //}

        public Dictionary<int, string> UserRoleDict = EnumUtilities.ConvertToDict<UserRole>();

        //protected override void OnInitialized()
        //{
        //    selectedRoleInt = (InitialRole is null) ? -1 : (int)InitialRole;
        //}
    }
}
