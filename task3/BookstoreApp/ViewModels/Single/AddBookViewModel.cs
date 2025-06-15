using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BookstoreApp.Helpers;
using BookstoreApp.Models;
using BookstoreApp.Models.Contexts;
using Microsoft.IdentityModel.Tokens;

namespace BookstoreApp.ViewModels.Single
{
    public class AddBookViewModel : WorkspaceViewModel, INotifyPropertyChanged
    {
        private string _topLabel;
        private string _title;
        private string _isbn;
        private string _errorLablel;

        public AddBookViewModel() : base("Add book")
        {
            AddBookCommand = new BaseCommand(() => AddNewBook());
            TopLabel = "Add new book";
            ErrorLabel = "";
        }

        public string TopLabel
        {
            get => _topLabel;
            set
            {
                _topLabel = value;
                OnPropertyChanged(nameof(TopLabel));
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string ISBN
        {
            get => _isbn;
            set
            {
                _isbn = value;
                OnPropertyChanged(nameof(ISBN));
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

        public ICommand AddBookCommand { get; }

        private void AddNewBook()
        {
            if (!Title.IsNullOrEmpty() && !ISBN.IsNullOrEmpty())
            {
                DatabaseContext db = new DatabaseContext();

                Book book = new Book
                {
                        Title = Title,
                        Isbn = ISBN,
                        CreationDateTime = DateTime.Now,
                        IsActive = true
                };
                db.Books.Add(book);
                db.SaveChanges();
                TopLabel = $"Book '{book.Title}' successfully added.";
                ErrorLabel = "";
                
            }
            else
            {
                ErrorLabel = "Unable to add a new book. Fill the fields and try again";
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
