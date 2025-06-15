using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Models.Dtos
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string? Street { get; set; } = null!;

        public string? HouseNumber { get; set; } = null!;

        public string? FlatNumber { get; set; } = null!;
        public string? PostalCode { get; set; } = null!;

        public string? PostalCity { get; set; } = null!;

        public string? CountyOrRegion { get; set; } = null!;

        public string? CountryName { get; set; } = null!;

        public string? PhoneNumber {  get; set; } = null!;
    }
}
