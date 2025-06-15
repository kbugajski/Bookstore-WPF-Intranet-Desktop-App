using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Models.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string BookCategoriesTitles { get; set; } = null!;
        public string BookPublishersTitles { get; set; } = null!;
        public string BookAuthorsTitles { get; set; } = null!;


        public string Isbn { get; set; }
    }
}
