using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Notes;

namespace WebServer.Service.Notes
{
    public interface INoteHttpRepository : IHttpRepository<NoteRequest>
    {
        Task<PagingResponse<NoteModel>> GetNotes(NoteParameters noteParameters);

        Task<bool> UpdateNote(NoteRequest model);
  
        Task UploadDelete(string path);
    }
}
