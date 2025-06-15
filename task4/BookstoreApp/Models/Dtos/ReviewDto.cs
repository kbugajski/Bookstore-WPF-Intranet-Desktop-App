using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Models.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public string BookName { get; set; }

        public string CustomerName { get; set; }

        public double Rating { get; set; }

        public string? ReviewText { get; set; }

        public DateTime ReviewDate { get; set; }
    }
}
