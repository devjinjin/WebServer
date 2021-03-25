using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebServer.Models.Notes;
using WebServer.Service.Notes;

namespace WebServer.Client.Pages.Note
{
    public partial class NoteUpdate
    {
        [Parameter]
        public string id { get; set; }


        private NoteRequest _noteRequest = new NoteRequest();
        private NoteModel _note = new NoteModel();
        private NoteSuccessNotification _notification;
        private string originFilePath = "";
        [Inject]
        public INoteHttpRepository Repository { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _note = await Http.GetFromJsonAsync<NoteModel>($"api/notes/{id}");


        }

        private async Task Modify()
        {
            if (_note.FilePath != null && _note.FilePath.Length > 0) {
                _noteRequest.Id = _note.Id;
                _noteRequest.Content = _note.Content;
                _noteRequest.Title = _note.Title;
                _noteRequest.Name = _note.Name;
                _noteRequest.CreatedBy = _note.CreatedBy;
                _noteRequest.FilePath = _note.FilePath;
                _noteRequest.OldFilePath = originFilePath;

                await Repository.UpdateNote(_noteRequest);

                _notification.Show();
            }

        }
        private void DeleteImage()
        {
            originFilePath = _note.FilePath;
            _note.FilePath = "";

        }
        
        private void AssignImageUrl(string imgUrl)
        {
            _noteRequest.isNewImage = true;
        
            _note.FilePath = imgUrl;
     
        }
    }
}
