using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Notes;
using WebServer.Service.Notes;

namespace WebServer.Client.Pages.Note
{
    public partial class NoteList
    {

        public List<NoteModel> Notes { get; set; } = new List<NoteModel>();
        public MetaData MetaData { get; set; } = new MetaData();

        private NoteParameters _NoteParameters = new NoteParameters();

        [Inject]
        public INoteHttpRepository Repository { get; set; }

        bool IsLoading { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            await GetNotes();
        }
        private async Task SelectedPage(int page)
        {
            _NoteParameters.PageNumber = page;
            IsLoading = true;
            await GetNotes();
        }

        private async Task GetNotes()
        {
            var pagingResponse = await Repository.GetNotes(_NoteParameters);
            Notes = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
            IsLoading = false;
        }

        private async Task SearchChanged(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _NoteParameters.PageNumber = 1;
            _NoteParameters.SearchTerm = searchTerm;
            IsLoading = true;
            await GetNotes();
        }

        private async Task SortChanged(string orderBy)
        {
            Console.WriteLine(orderBy);
            _NoteParameters.OrderBy = orderBy;
            IsLoading = true;
            await GetNotes();
        }       
    }
}
