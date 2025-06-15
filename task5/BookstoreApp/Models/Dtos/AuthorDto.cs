using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Models.Dtos
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        //public string Title { get; set; } = null!;
        public string? Biography { get; set; } 
        public string? CountryOrigin { get; set; }

        public bool IsAlive { get; set; }
    }
}
