using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Models.Dtos;
using BookstoreApp.Models.Services;
using BookstoreApp.Models;

namespace BookstoreApp.ViewModels.Single
{
    public class AddCountryViewModel : BaseCreateViewModel<CountryService, CountryDto, Country>
    {

        public AddCountryViewModel() : base("Add Country")
        {
            TopLabel = "Add new Country";
        }

        public AddCountryViewModel(Country model) : base("Edit Country", model)
        {
            TopLabel = "Edit Country";
        }



        public string Title
        {
            get => Model.Title;
            set
            {
                if (Model.Title != value)
                {
                    Model.Title = value;
                    OnPropertyChanged(() => Title);
                }

            }
        }

    }   
    }
