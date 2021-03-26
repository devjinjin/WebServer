using Microsoft.AspNetCore.Components;

namespace WebServer.Client.Pages.Place
{
    public partial class PlaceSuccessNotification
    {
        private string _modalDisplay;
        private string _modalClass;
        private bool _showBackdrop;
        [Inject]
        public NavigationManager Navigation { get; set; }

        public void Show()
        {
            _modalDisplay = "block;";
            _modalClass = "show";
            _showBackdrop = true;
            StateHasChanged();
        }

        private void Hide()
        {
            _modalDisplay = "none;";
            _modalClass = "";
            _showBackdrop = false;
            StateHasChanged();
            Navigation.NavigateTo("/place");
        }
    }
}
