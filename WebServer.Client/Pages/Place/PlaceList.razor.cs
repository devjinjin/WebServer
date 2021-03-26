using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Places;
using WebServer.Service.Places;

namespace WebServer.Client.Pages.Place
{
    public partial class PlaceList
    {
        public List<PlaceInfo> places { get; set; } = new List<PlaceInfo>();

        private PlaceParameters Parameters = new PlaceParameters();

        [Inject]
        public IPlaceHttpRepository Repository { get; set; }

        bool IsLoading { get; set; } = false;


        bool StopLoading = false;

        int pageNumber = 1;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadMore();
                await InitJsListenerAsync();
            }
        }

        protected async Task InitJsListenerAsync()
        {
            await JsRuntime.InvokeVoidAsync("ScrollList.Init", "list-end", DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public async Task LoadMore()
        {
            if (!IsLoading)
            {
                IsLoading = true;

                StateHasChanged();

                await Task.Delay(1000);
                Parameters.PageNumber = pageNumber;
                var pagingResponse = await Repository.GetItems(Parameters);

                if (pagingResponse.Items.Count > 0)
                {
                    places.AddRange(pagingResponse.Items);
                    pageNumber++;
                }
                else
                {
                    StopLoading = true;
                }


                IsLoading = false;

                StateHasChanged();


                //at the end of pages or results stop loading anymore
                if (StopLoading)
                {
                    await StopListener();
                }
            }
        }

        public async Task StopListener()
        {

            IsLoading = false;
            await JsRuntime.InvokeVoidAsync("ScrollList.RemoveListener");
            StateHasChanged();
        }


        public void Dispose()
        {
            JsRuntime.InvokeVoidAsync("ScrollList.RemoveListener");
        }
    }
    
}
