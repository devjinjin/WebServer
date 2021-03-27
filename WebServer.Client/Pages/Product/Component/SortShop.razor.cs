using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace WebServer.Client.Pages.Product.Component
{
    public partial class SortShop
    {
        [Parameter]
        public EventCallback<string> OnSortChanged { get; set; }

        private async Task ApplySort(ChangeEventArgs eventArgs)
        {
            if (eventArgs.Value.ToString() == "-1")
                return;

            await OnSortChanged.InvokeAsync(eventArgs.Value.ToString());
        }
    }
}
