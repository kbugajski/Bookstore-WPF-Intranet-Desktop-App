using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Models.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string? Code { get; set; } = null!;
        public string? CountryName { get; set; } = null!;
        public string? Nip { get; set; } = null!;
        public string? Title { get; set; } = null!;
        public string? CustomerStatus { get; set; } = null!;
        public string? CustomerCategory { get; set; } = null!;
        public string? Address { get; set; } = null!;
    }
}
