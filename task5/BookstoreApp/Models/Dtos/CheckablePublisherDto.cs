using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Models.Dtos
{
    public class CheckablePublisherDto : PublisherDto
    {
        public bool IsChecked { get; set; }
    }
}
