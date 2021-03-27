using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Models.Category
{
    public class CategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public int OrderNum { get; set; } = 0;

        public bool IsHide { get; set; } = false;

        public DateTime Created { get; set; }
    }
}
