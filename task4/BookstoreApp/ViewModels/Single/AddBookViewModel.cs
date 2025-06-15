using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BookstoreApp.Helpers;
using BookstoreApp.Models;
using BookstoreApp.Models.Contexts;
using BookstoreApp.Models.Dtos;
using BookstoreApp.Models.Services;
using Microsoft.IdentityModel.Tokens;

namespace BookstoreApp.ViewModels.Single
{
    public class AddBookViewModel : BaseCreateViewModel<BookService, BookDto, Book>
    {
        private string _errorLablel;

        public AddBookViewModel() : base("Add book")
        {
            ErrorLabel = "";
        }



        public string? Title
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

        public string Isbn
        {
            get => Model.Isbn;
            set
            {
                if(Model.Isbn != value)
                OnPropertyChanged(() => Isbn);
            }
        }

        public string ErrorLabel
        {
            get => _errorLablel;
            set
            {
                _errorLablel = value;
                OnPropertyChanged(nameof(ErrorLabel));
            }
        }





        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

    }
}
