using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Helpers;
using BookstoreApp.Models.Dtos;
using BookstoreApp.ViewModels.Many;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;
using BookstoreApp.Models.Services;
using BookstoreApp.Models;
using Microsoft.IdentityModel.Tokens;

namespace BookstoreApp.ViewModels.Single
{
    public class AddAddressViewModel : BaseCreateViewModel<AddressService, AddressDto, Address>
    {
        public AddAddressViewModel() : base("Add Address")
        {
            TopLabel = "Add new address";
            SelectCountryCommand = new BaseCommand(() => SelectCountry());
            CountryTitle = "Select country";
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<CountryDto>>(this, (recipient, message) => GetSelectedCountry(message));

        }
        public AddAddressViewModel(Address model) : base("Edit Address", model)
        {
            TopLabel = "Edit address";
            SelectCountryCommand = new BaseCommand(() => SelectCountry());
            CountryTitle = Model.Country?.Title ?? "Select Country";
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<CountryDto>>(this, (recipient, message) => GetSelectedCountry(message));

        }


        public ICommand SelectCountryCommand { get; set; }



        public string? FlatNumber
        {
            get => Model.FlatNumber;
            set
            {
              if (Model.FlatNumber != value)
                {
                    Model.FlatNumber = value;
                    OnPropertyChanged(() => FlatNumber);
                }
            }
        }
        public string? Street
        {
            get => Model.Street;
            set
            {
                if (Model.Street != value)
                {
                    Model.Street = value;
                    OnPropertyChanged(() => Street);
                }
            }
                    
        }

        public string? HouseNumber
        {
            get => Model.HouseNumber;
            set
            {
                if (Model.HouseNumber != value)
                {
                    Model.HouseNumber = value;
                    OnPropertyChanged(() => HouseNumber);
                }
            }
        }

        public string? PostalCode
        {
            get => Model.PostalCode;
            set
            {
                if (Model.PostalCode != value)
                {
                    Model.PostalCode = value;
                    OnPropertyChanged(() => PostalCode);
                }
            }
        }

        public string? PostalCity
        {
            get => Model.PostalCity;
            set
            {
                if (Model.PostalCity != value)
                {
                    Model.PostalCity = value;
                    OnPropertyChanged(() => PostalCity);
                }
            }
        }

        public string? CountyOrRegion
        {
            get => Model.CountyOrRegion;
            set
            {
                if (Model.CountyOrRegion != value)
                {
                    Model.CountyOrRegion = value;
                    OnPropertyChanged(() => CountyOrRegion);
                }
            }
        }

        public int? CountryId
        {
            get => Model.CountryId;
            set
            {
                if (Model.CountryId != value)
                {
                    Model.CountryId = value;
                    OnPropertyChanged(() => CountryId);
                }
            }
        }

        public string? PhoneNumber
        {
            get => Model.PhoneNumber;
            set
            {
                if (Model.PhoneNumber != value)
                {
                    Model.PhoneNumber = value;
                    OnPropertyChanged(() => PhoneNumber);
                }
            }
        }
        private string _CountryTitle;
        public string CountryTitle
        {
            get => _CountryTitle;
            set
            {
                if (value != _CountryTitle)
                {
                    _CountryTitle = value;
                    OnPropertyChanged(() => CountryTitle);
                }
            }
        }



        private void SelectCountry()
        {
            //WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            //{
            //    Sender = this,
            //    ViewModelToBeOpened = new CountriesWithCallbackViewModel(this)
            //});
            WindowManager.OpenWindow(new CountriesWithCallbackViewModel(this));
        }

        private void GetSelectedCountry(SelectedObjectMessage<CountryDto> message)
        {
            if (message.WhoRequestedToSelect == this)
            {
                CountryId = message.SelectedObject.Id;
                CountryTitle = message.SelectedObject.Title;
            }
        }


    }
}