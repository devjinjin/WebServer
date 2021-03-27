using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace WebServer.Client.Pages.Notice.Component
{
    public partial class SortNotice
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
