using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Places;
using WebServer.Service.Places;

namespace WebServer.Client.Pages.Admin.Place
{
    public partial class AdminPlaceList
    {
        public List<PlaceInfo> PlaceInfoList { get; set; } = new List<PlaceInfo>();
        public MetaData MetaData { get; set; } = new MetaData();

        private PlaceParameters _placeParameters = new PlaceParameters();

        [Inject]
        public IPlaceHttpRepository repository { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await GetPlaces();
        }

        private async Task SelectedPage(int page)
        {
            _placeParameters.PageNumber = page;
            await GetPlaces();
        }

        private async Task GetPlaces()
        {

            var pagingResponse = await repository.GetItems(_placeParameters);
            PlaceInfoList = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task SearchChanged(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _placeParameters.PageNumber = 1;
            _placeParameters.SearchTerm = searchTerm;
            await GetPlaces();
        }

        private async Task SortChanged(string orderBy)
        {
            Console.WriteLine(orderBy);
            _placeParameters.OrderBy = orderBy;
            await GetPlaces();
        }
    }
}
