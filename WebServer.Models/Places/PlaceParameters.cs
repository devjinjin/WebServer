﻿using WebServer.Models.Features;

namespace WebServer.Models.Places
{
    public class PlaceParameters : PageParameters
    {

        public PlaceParameters() { 
            //상속 형식에서 값이 바뀔수 있는 것들에 대한 정의 필요
            OrderBy = "created desc";
        }
    }
}
