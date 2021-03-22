using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Models.Notes
{
    public class NoteResponse
    {
        public string Id { get; set; }
        public bool IsSuccess { get; set; }

        public string FilePath { get; set; }
    }
}
