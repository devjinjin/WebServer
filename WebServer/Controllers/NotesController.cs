using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Data;
using WebServer.Data.Notes;
using WebServer.Models.Notes;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
 
        private readonly IWebHostEnvironment _environment;
        private readonly INoteRepository _noteRepository;
        public NotesController(IWebHostEnvironment environment, INoteRepository noteRepository)
        {
            _environment = environment;
            _noteRepository = noteRepository;
        }

        /// <summary>
        /// 전체 리스트 가져오기
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteResponse>>> GetNoteAsync()
        {
            List<NoteResponse> noteResponses = new List<NoteResponse>();
            var list = await _noteRepository.GetNoteList();

            foreach (var note in list)
            {

                noteResponses.Add(new NoteResponse()
                {
                    Id = note.Id.ToString(),
                    FilePath = note.FilePath,
                    Content = note.Content,
                    Title = note.Title,
                    Create = note.Create,
                    Modify = note.Modify
                });
            }

            return noteResponses;
        }

        /// <summary>
        /// 등록하기
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<NoteResponse>> PostNote([FromForm]NoteRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var res = await AddReader(model);

            // On successful submission
            // redirect to the Master Page
            return res;
        }



        private async Task<NoteResponse> AddReader(NoteRequest model)
        {
            var res = new NoteResponse();

            // magic happens here
            // check if model is not empty
            if (model != null)
            {
                // create new entity
                var note = new Note();

                // add non-file attributes
                note.Title = model.Title;
                note.Content = model.Content;
                note.Create = DateTime.Now;
                // check if any file is uploaded
                var work = model.File;

                if (work != null)
                {
                    // get the file extension and 
                    // create a new File Name using Guid
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(work.FileName)}";

                    // create full file path using
                    // the IHostEnvironment.ContentRootPath
                    // which is basically the execution directory
                    // and append a sub directory workFiles 
                    // [Should be present before hand!!!]
                    // and lastly append the file name
                    var filePath = Path.Combine(_environment.ContentRootPath, "Files", fileName);

                    // open-create the file in a stream and
                    // copy the uploaded file content into
                    // the new file (IFormFile contains a stream)
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await work.CopyToAsync(fileSteam);
                    }

                    // assign the generated filePath to the 
                    // workPath property in the entity
                    note.FilePath = $"{Request.Scheme}://{Request.Host}/Files/{fileName}";
                }

                // add the created entity to the datastore
                // using a Repository class IReadersRepository
                // which is registered as a Scoped Service
                // in Startup.cs
                var created = _noteRepository.AddNote(note);

                // Set the Success flag and generated details
                // to show in the View 
                res.Id = created.Id.ToString();
                res.FilePath = created.FilePath;
                res.Content = created.Content;
                res.Title = created.Title;
                res.Modify = created.Modify;
                res.Create = created.Create;
            }

            // return the model back to view
            // with added changes and flags
            return res;
        }
    }
}
