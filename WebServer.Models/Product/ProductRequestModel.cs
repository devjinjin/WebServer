using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebServer.Models.Product
{
    public class ProductRequestModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Discription { get; set; }

        public string Supplier { get; set; }


        public double Price { get; set; }

        public int TotalCount { get; set; } = 0;

        public string ImageUrl { get; set; }

        public bool IsSoldOut { get; set; } = false;

        public bool IsNewImage { get; set; } = false;

        public string OldFilePath { get; set; }
    }
}
