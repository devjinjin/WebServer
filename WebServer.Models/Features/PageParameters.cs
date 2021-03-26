using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Models.Features
{
    public class PageParameters
    {
        public const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        public int _pageSize = 4;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string SearchTerm { get; set; }
        public string OrderBy { get; set; } = "name";
    }
}
