using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Notices;
using WebServer.Service.Common.Notices;

namespace WebServer.Client.Pages.Notice
{
    public partial class NoticeList
    {
        public List<NoticeModel> notices { get; set; } = new List<NoticeModel>();
        public MetaData MetaData { get; set; } = new MetaData();

        private NoticParameters Parameters = new NoticParameters();

        [Inject]
        public INoticeHttpRepository Repository { get; set; }

        private int Index = 1;
   

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

            notices = pagingResponse.Items;
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
