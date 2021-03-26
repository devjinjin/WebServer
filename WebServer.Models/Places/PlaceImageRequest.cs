using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Models.Places
{
    public class PlaceImageRequest
    {
        public string FileName { get; set; }

        public string ImageMemo { get; set; }

        public IFormFile File { get; set; }
    }
}
