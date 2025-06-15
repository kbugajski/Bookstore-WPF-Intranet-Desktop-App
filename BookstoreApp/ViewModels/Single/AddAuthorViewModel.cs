using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BookstoreApp.Helpers;
using BookstoreApp.Models;
using BookstoreApp.Models.Contexts;
using BookstoreApp.Models.Dtos;
using BookstoreApp.Models.Services;
using BookstoreApp.ViewModels.Many;
using CommunityToolkit.Mvvm.Messaging;

namespace BookstoreApp.ViewModels.Single
{
    public class AddAuthorViewModel : BaseCreateViewModel<AuthorService,AuthorDto,Author>, INotifyPropertyChanged
    {
        private string _topLabel;
        public ICommand SelectCountryCommand { get; set; }


        public AddAuthorViewModel() : base("Add Author")
        {
            TopLabel = "Add New Author";
            Model.IsAlive = true; // Default 
            CountryTitle = "Select Country";
            SelectCountryCommand = new BaseCommand(() => SelectCountry());
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<CountryDto>>(this, (recipient, message) => GetSelectedCountry(message));
        }

        public AddAuthorViewModel(Author model) : base("Edit Author", model)
        {
            TopLabel = "Edit Author";
            CountryTitle = Model.Country?.Title ?? "Select Country";
            SelectCountryCommand = new BaseCommand(() => SelectCountry());
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<CountryDto>>(this, (recipient, message) => GetSelectedCountry(message));
        }

       

        public string FirstName
        {
            get => Model.FirstName;
            set
            {
                if (Model.FirstName != value)
                {
                    Model.FirstName = value;
                    OnPropertyChanged(() => FirstName);
                }
            }
        }

        public string LastName
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

        public bool IsAlive
        {
            get => Model.IsAlive;
            set
            {
                if (Model.IsAlive != value)
                {
                    Model.IsAlive = value;
                    OnPropertyChanged(() => IsAlive);
                }
            }
        }

        public string? Biography
        {
            get => Model.Biography;
            set
            {
                if (Model.Biography != value)
                {
                    Model.Biography = value;
                    OnPropertyChanged(() => Biography);
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
