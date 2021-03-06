using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebServer.Models.Product
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required field")]
        public string Name { get; set; }

        public string Discription { get; set; }

        [Required(ErrorMessage = "Supplier is required field")]
        public string Supplier { get; set; }


        [Range(1, double.MaxValue, ErrorMessage = "Value for the Price can't be lower than 1")]
        public double Price { get; set; }


        public string ImageUrl { get; set; }

        public int ReadCnt { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public int TotalCount { get; set; } = 0;

        public bool IsSoldOut { get; set; } = false;

    }
}
