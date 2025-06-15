using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Helpers;

namespace BookstoreApp.Models.Dtos
{
    public class YesNoComboBoxItemDto
    {
        public YesNoEnum SelectedOption { get; set; }
        public string OptionName { get; set; } = default!;
    }
}
