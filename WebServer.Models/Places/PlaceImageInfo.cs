using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Models.Places
{
    public class PlaceImageInfo
    {
        public int Id { get; set; }

        public int InfoId { get; set; }

        public string FileName { get; set; }

        public string ImageMemo { get; set; }

        public DateTime RegistDate { get; set; }
    }
}
