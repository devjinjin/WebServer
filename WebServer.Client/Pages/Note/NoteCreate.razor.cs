using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebServer.Client.Shared;
using WebServer.Models.Product;
using WebServer.Service.Products;
using WebServer.Client.Pages.Component;
using WebServer.Service.Notes;
using WebServer.Models.Notes;

namespace WebServer.Client.Pages.Note
{

    public partial class NoteCreate
    {

        private NoteRequest _note = new NoteRequest();
        private NoteSuccessNotification _notification;

        [Inject]
        public INoteHttpRepository Repository { get; set; }

        private async Task Create()
        {
            await Repository.CreateNote(_note);
            _notification.Show();
        }

        private void AssignImageUrl(string imgUrl) {
            _note.FilePath = imgUrl;
        }
    }
}
