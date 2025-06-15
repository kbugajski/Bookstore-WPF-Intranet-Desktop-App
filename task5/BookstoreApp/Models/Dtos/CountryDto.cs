using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Models.Dtos
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public DateTime CreationDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
        public DateTime? DeleteDateTime { get; set; }
    }
}
