using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Models.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime CreationDateTime { get; set; }
        public DateTime? EditDateTime { get; set; }
        public DateTime? DeleteDateTime { get; set; }
    }
}
