using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Notices;
using WebServer.Service.Common.Notices;

namespace WebServer.Client.Pages.Admin.Notice
{
    public partial class AdminNoticeList
    {
        public List<NoticeModel> notices { get; set; } = new List<NoticeModel>();
        public MetaData MetaData { get; set; } = new MetaData();

        private NoticeParameters Parameters = new NoticeParameters();

        [Inject]
        public INoticeHttpRepository Repository { get; set; }

        //private int Index = 0;


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
