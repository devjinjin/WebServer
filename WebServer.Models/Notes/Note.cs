using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Models.Notes
{
    public class Note
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string FilePath { get; set; }

        public DateTime Create { get; set; }

        public DateTime Modify { get; set; }

    }
}
