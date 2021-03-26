using Microsoft.AspNetCore.Components;
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
        public MetaData MetaData { get; set; } = new MetaData();

        private PlaceParameters Parameters = new PlaceParameters();

        [Inject]
        public IPlaceHttpRepository Repository { get; set; }



        protected override async Task OnInitializedAsync()
        {
            await GetItem();
        }
        private async Task SelectedPage(int page)
        {
            Parameters.PageNumber = page;
            await GetItem();
        }

        private async Task GetItem()
        {

            var pagingResponse = await Repository.GetItems(Parameters);
            places = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task SearchChanged(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            Parameters.PageNumber = 1;
            Parameters.SearchTerm = searchTerm;
            await GetItem();
        }

        private async Task SortChanged(string orderBy)
        {
            Console.WriteLine(orderBy);
            Parameters.OrderBy = orderBy;
            await GetItem();
        }
    }
}
