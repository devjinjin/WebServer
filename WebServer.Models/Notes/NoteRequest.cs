using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Models.Notes
{
    public class NoteRequest
    {
 
        public string Title { get; set; }

        public string Content { get; set; }

        public IFormFile File { get; set; }

    }
}
