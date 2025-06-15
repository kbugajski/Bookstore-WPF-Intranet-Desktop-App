using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Helpers;
using BookstoreApp.Models.Contexts;
using BookstoreApp.Models;
using System.Windows.Input;
using BookstoreApp.Models.Services;
using BookstoreApp.Models.Dtos;
using BookstoreApp.ViewModels.Single;
using CommunityToolkit.Mvvm.Messaging;

namespace BookstoreApp.ViewModels.Many
{
    public class CountriesViewModel : BaseManyViewModel<CountryService, CountryDto, Country>
    {
        public CountriesViewModel() : base("Countries")
        {

        }

        protected override void ClearFilter()
        {
            throw new NotImplementedException();
        }

        protected override void CreateNew()
        {
        }


    }
}
