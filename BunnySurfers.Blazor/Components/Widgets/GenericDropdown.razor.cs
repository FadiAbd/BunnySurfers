using Microsoft.AspNetCore.Components;

namespace BunnySurfers.Blazor.Components.Widgets
{
    public partial class GenericDropdown
    {
        [Parameter]
        public required string ForName { get; set; }
        [Parameter]
        public string LabelText { get; set; } = string.Empty;
        [Parameter]
        public string DefaultOptionText { get; set; } = string.Empty;
        [Parameter]
        public int? InitialKey { get; set; } = null;
        [Parameter]
        public Dictionary<int, string> ItemDict { get; set; } = [];
        [Parameter]
        public EventCallback<int> OnValueChanged { get; set; }

        private int selectedKey;
        public int SelectedKey
        {
            get => selectedKey;
            set
            {
                selectedKey = value;
                OnValueChanged.InvokeAsync(value);
            }
        }

        protected override void OnInitialized()
        {
            selectedKey = (InitialKey is null) ? -1 : (int)InitialKey;
        }
    }
}
