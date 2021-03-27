using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebServer.Models.Notes;
using WebServer.Service.Notes;

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
            await Repository.Create(_note);
            _notification.Show();
        }

        private void AssignImageUrl(string imgUrl) {
            _note.FilePath = imgUrl;
        }
    }
}
