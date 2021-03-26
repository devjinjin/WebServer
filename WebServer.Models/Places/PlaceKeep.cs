using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Models.Places
{
    public class PlaceKeep
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public int PlaceId { get; set; }

        public DateTime RegistDate { get; set; }
    }
}
