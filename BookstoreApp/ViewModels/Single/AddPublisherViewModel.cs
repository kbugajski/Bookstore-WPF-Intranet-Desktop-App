using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Models;
using BookstoreApp.Models.Dtos;
using BookstoreApp.Models.Services;

namespace BookstoreApp.ViewModels.Single
{
    public class AddPublisherViewModel : BaseCreateViewModel<PublisherService, PublisherDto, Publisher>
    {
        public AddPublisherViewModel() : base("Add Publisher")
        {
            TopLabel = "Add new publisher";
        }

        public AddPublisherViewModel(Publisher model) : base("Edit Publisher", model)
        {
            TopLabel = "Edit publisher";
        }


        public string? Name
        {
            get => Model.Name;
            set
            {
                if (Model.Name != value)
                {
                    Model.Name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }
        public string? Nip
        {
            get => Model.Nip;
            set
            {
                if (Model.Nip != value)
                {
                    Model.Nip = value;
                    OnPropertyChanged(() => Nip);
                }
            }
        }

       
    }
}


