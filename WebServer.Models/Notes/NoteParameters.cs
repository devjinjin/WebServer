using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Models.Features;

namespace WebServer.Models.Notes
{
    public class NoteParameters : PageParameters
    {
        public NoteParameters()
        {
            OrderBy = "Created desc";
            //상속 형식에서 값이 바뀔수 있는 것들에 대한 정의 필요
        }
    }
}
