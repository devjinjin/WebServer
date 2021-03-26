using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebServer.Data.Notes;
using WebServer.Models.Notes;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
 
    
        private readonly INoteRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NotesController(INoteRepository repository, IWebHostEnvironment webHostEnvironment, ILogger<NotesController> logger)
        {
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
        }

        ///// <summary>
        ///// 전체 리스트 가져오기
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<NoteResponse>>> GetNoteAsync()
        //{
        //    List<NoteResponse> noteResponses = new List<NoteResponse>();
        //    var list = await _noteRepository.GetAllAsync();

        //    foreach (var note in list)
        //    {

        //        noteResponses.Add(new NoteResponse()
        //        {
        //            Id = note.Id.ToString(),
        //            FilePath = note.FilePath,
        //            Content = note.Content,
        //            Title = note.Title,
        //            Create = note.Created,
        //            Modify = note.Modified
        //        });
        //    }

        //    return noteResponses;
        //}

        ///// <summary>
        ///// 입력
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Consumes("multipart/form-data")]
        //public async Task<ActionResult<NoteResponse>> PostNote([FromForm]NoteRequest model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    var res = await AddNote(model);

        //    //// On successful submission
        //    //// redirect to the Master Page
        //    //return res;
        //    var uri = Url.Link("GetNoteById", new { id = res.Id });
        //    return Created(uri, res); // 201 Created
        //}


        //// 상세
        //// GET api/Notices/1
        //[HttpGet("{id}", Name = "GetNoteById")]
        //public async Task<IActionResult> GetById([FromRoute] int id)
        //{
        //        var model = await _noteRepository.GetByIdAsync(id);
        //        return Ok(model);
        //}


        //private async Task<NoteResponse> AddNote(NoteRequest model)
        //{
        //    var res = new NoteResponse();

        //    // magic happens here
        //    // check if model is not empty
        //    if (model != null)
        //    {
        //        // create new entity
        //        var note = new NoteModel();

        //        // add non-file attributes
        //        note.Title = model.Title;
        //        note.Content = model.Content;
        //        note.Created = DateTime.Now;
        //        // check if any file is uploaded
        //        var work = model.File;

        //        if (work != null)
        //        {
        //            // get the file extension and 
        //            // create a new File Name using Guid
        //            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(work.FileName)}";

        //            // create full file path using
        //            // the IHostEnvironment.ContentRootPath
        //            // which is basically the execution directory
        //            // and append a sub directory workFiles 
        //            // [Should be present before hand!!!]
        //            // and lastly append the file name
        //            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Files", fileName);

        //            // open-create the file in a stream and
        //            // copy the uploaded file content into
        //            // the new file (IFormFile contains a stream)
        //            using (var fileSteam = new FileStream(filePath, FileMode.Create))
        //            {
        //                await work.CopyToAsync(fileSteam);
        //            }

        //            // assign the generated filePath to the 
        //            // workPath property in the entity
        //            note.FilePath = $"{Request.Scheme}://{Request.Host}/Files/{fileName}";
        //        }

        //        // add the created entity to the datastore
        //        // using a Repository class IReadersRepository
        //        // which is registered as a Scoped Service
        //        // in Startup.cs
        //        var created = _noteRepository.AddAsync(note);

        //        // Set the Success flag and generated details
        //        // to show in the View 
        //        res.Id = created.Id.ToString();
        //        res.FilePath = note.FilePath;
        //        res.Content = note.Content;
        //        res.Title = note.Title;
        //        res.Modify = note.Modified;
        //        res.Create = note.Created;
        //    }

        //    // return the model back to view
        //    // with added changes and flags
        //    return res;
        //}

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] NoteParameters productParameters)
        {

            var notes = await _repository.GetAllAsync(productParameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(notes.MetaData));

            return Ok(notes);
        }

        // 상세
        // GET api/Notices/1
        [HttpGet("{id}", Name = "GetNoteById")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var model = await _repository.GetByIdAsync(Int32.Parse(id));
            return Ok(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] NoteRequest note)
        {
            if (note == null)
            {
                return BadRequest();
            }

            await _repository.AddAsync(note);

            return CreatedAtAction("GetNoteById", new { id = note.Id }, note);
        }


        [HttpPut]
        public async Task<bool> PutNote([FromBody] NoteRequest noteRequest)
        {
            return await EditNote(noteRequest);


        }

        private async Task<bool> EditNote(NoteRequest model)
        {

            if (model.isNewImage)
            {
                var OldFilePath = model.OldFilePath;

                if (OldFilePath.Length > 0)
                {
                    var urlArray = OldFilePath.Split("/");
                    if (urlArray.Length > 0)
                    {
                        var fileName = urlArray[urlArray.Length - 1];
                        if (System.IO.File.Exists($"Files/Product/{fileName}"))
                        {
                            System.IO.File.Delete($"Files/Product/{fileName}");
                        }
                    }
                }
            }

            return await _repository.EditAsync(model);

        }
    }
}
