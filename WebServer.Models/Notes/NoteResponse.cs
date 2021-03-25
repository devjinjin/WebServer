using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Models.Notes
{
    public class NoteResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }


        public string Title { get; set; }

        public string Content { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        public string FilePath { get; set; }



    }
}
