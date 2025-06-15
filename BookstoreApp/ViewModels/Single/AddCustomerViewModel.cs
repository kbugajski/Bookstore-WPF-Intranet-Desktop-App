using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Helpers;
using BookstoreApp.Models.Dtos;
using BookstoreApp.Models.Services;
using BookstoreApp.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;
using BookstoreApp.Models.Contexts;
using BookstoreApp.ViewModels.Many;

namespace BookstoreApp.ViewModels.Single
{
    public class AddCustomerViewModel : BaseCreateViewModel<CustomerService, CustomerDto, Customer>
    {
            public ICommand SelectCountryCommand { get; set; }
            
        
           
        public string? FirstName
        {
            get => Model.FirstName;
            set{ 
                if (Model.FirstName != value)
                {
                    Model.FirstName = value;
                    OnPropertyChanged(() => FirstName);
                }
            }
        }

        public string? LastName
        {
            get => Model.LastName;
            set
            {
                if (Model.LastName != value)
                {
                    Model.LastName = value;
                    OnPropertyChanged(() => LastName);
                }
            }
        }

        public string? Code
            {
                get => Model.Code;
                set
                {
                    if (Model.Code != value)
                    {
                        Model.Code = value;
                        OnPropertyChanged(() => Code);
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

            public int CustomerCategoryId
            {
                get => Model.CustomerCategoryId;
                set
                {
                    if (Model.CustomerCategoryId != value)
                    {
                        Model.CustomerCategoryId = value;
                        OnPropertyChanged(() => CustomerCategoryId);
                    }
                }
            }

            public int CustomerStatusId
            {
                get => Model.CustomerStatusId;
                set
                {
                    if (Model.CustomerStatusId != value)
                    {
                        Model.CustomerStatusId = value;
                        OnPropertyChanged(() => CustomerStatusId);
                    }
                }
            }

            public string? Street
            {
                get => Model.Address?.Street;
                set
                {
                    if (Model.Address.Street != value)
                    {
                        Model.Address.Street = value;
                        OnPropertyChanged(() => Street);
                    }
                }
            }

            public string? HouseNumber
            {
                get => Model.Address?.HouseNumber;
                set
                {
                    if (Model.Address.HouseNumber != value)
                    {
                        Model.Address.HouseNumber = value;
                        OnPropertyChanged(() => HouseNumber);
                    }
                }
            }

            public string? FlatNumber
            {
                get => Model.Address?.FlatNumber;
                set
                {
                    if (Model.Address.FlatNumber != value)
                    {
                        Model.Address.FlatNumber = value;
                        OnPropertyChanged(() => FlatNumber);
                    }
                }
            }

            public string? PostalCode
            {
                get => Model.Address?.PostalCode;
                set
                {
                    if (Model.Address.PostalCode != value)
                    {
                        Model.Address.PostalCode = value;
                        OnPropertyChanged(() => PostalCode);
                    }
                }
            }

            public string? PostalCity
            {
                get => Model.Address?.PostalCity;
                set
                {
                    if (Model.Address.PostalCity != value)
                    {
                        Model.Address.PostalCity = value;
                        OnPropertyChanged(() => PostalCity);
                    }
                }
            }

            public string? CountyOrRegion
            {
                get => Model.Address?.CountyOrRegion;
                set
                {
                    if (Model.Address.CountyOrRegion != value)
                    {
                        Model.Address.CountyOrRegion = value;
                        OnPropertyChanged(() => CountyOrRegion);
                    }
                }
            }

            public int? CountryId
            {
                get => Model.Address?.CountryId;
                set
                {
                    if (Model.Address.CountryId != value)
                    {
                        Model.Address.CountryId = value;
                        OnPropertyChanged(() => CountryId);
                    }
                }
            }

            public string? PhoneNumber
            {
                get => Model.Address?.PhoneNumber;
                set
                {
                    if (Model.Address.PhoneNumber != value)
                    {
                        Model.Address.PhoneNumber = value;
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

        public AddCustomerViewModel() : base("Add customer")
        {
            TopLabel = "Add new customer";
            SelectCountryCommand = new BaseCommand(() => SelectCountry());
            //closeCommand = new BaseCommand(() => Cancel());
            CountryTitle = "Select country";
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<CountryDto>>(this, (recipient, message) => GetSelectedCountry(message));

        }

        public AddCustomerViewModel(Customer model) : base("Edit Customer", model)
        { 
            TopLabel = "Edit Customer";
            SelectCountryCommand = new BaseCommand(() => SelectCountry());
            //closeCommand = new BaseCommand(() => Cancel());
            CountryTitle = Model.Address.Country?.Title ?? "Select Country";
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<CountryDto>>(this, (recipient, message) => GetSelectedCountry(message));

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

