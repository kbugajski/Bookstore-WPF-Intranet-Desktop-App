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
            SearchInput = string.Empty;
            Refresh();
        }

        protected override void CreateNew()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new AddCountryViewModel()
            });
        }

        protected override void Edit()
        {
            if (SelectedModel != null)
            {
                Country modelToEdit = Service.GetModel(SelectedModel.Id);
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
                {
                    Sender = this,
                    ViewModelToBeOpened = new AddCountryViewModel(modelToEdit)
                });
            }
        }
    }
}
