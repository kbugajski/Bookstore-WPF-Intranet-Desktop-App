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

        public string? Country
        {
            get => Model.Address;
            set
            {
                if (Model.Address != value)
                {
                    Model.Address = value;
                    OnPropertyChanged(() => Country);
                }
            }
        }
    }
}


