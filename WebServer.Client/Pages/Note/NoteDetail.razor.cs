using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebServer.Models.Notes;

namespace WebServer.Client.Pages.Note
{
    public partial class NoteDetail
    {
        [Parameter]
        public string id { get; set; }

        private NoteModel note;

        protected override async Task OnInitializedAsync()
        {
            note = await Http.GetFromJsonAsync<NoteModel>($"api/notes/{id}");
        }


    }
}
