using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Notes;
using WebServer.Service.Notes;
using WebServer.Service.Products;

namespace WebServer.Client.Pages.Note
{
    public partial class NoteList
    {

        public List<NoteModel> Notes { get; set; } = new List<NoteModel>();
        public MetaData MetaData { get; set; } = new MetaData();

        private NoteParameters _NoteParameters = new NoteParameters();

        [Inject]
        public INoteHttpRepository Repository { get; set; }

        private int Index = 1;

        protected override async Task OnInitializedAsync()
        {
            await GetNotes();
        }
        private async Task SelectedPage(int page)
        {
            _NoteParameters.PageNumber = page;
            await GetNotes();
        }

        private async Task GetNotes()
        {

            var pagingResponse = await Repository.GetNotes(_NoteParameters);
            Notes = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task SearchChanged(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _NoteParameters.PageNumber = 1;
            _NoteParameters.SearchTerm = searchTerm;
            await GetNotes();
        }

        private async Task SortChanged(string orderBy)
        {
            Console.WriteLine(orderBy);
            _NoteParameters.OrderBy = orderBy;
            await GetNotes();
        }

        public void MoveCreate()
        {
            //this.NavigationManager.NavigateTo("/note/create", forceLoad: true);
            this.NavigationManager.NavigateTo("/note/create");

        }

        public void MoveUpdate(Guid id)
        {
            this.NavigationManager.NavigateTo("/note/update" + id);

        }

        public void MoveDelete(Guid id)
        {
            //this.NavigationManager.NavigateTo("/note/create", forceLoad: true);
            this.NavigationManager.NavigateTo("/note/delete/" + id);

        }

        public void MoveDetail()
        {
            //this.NavigationManager.NavigateTo("/note/create", forceLoad: true);
            this.NavigationManager.NavigateTo("/note/detail/" + 1);

        }
    }
}
