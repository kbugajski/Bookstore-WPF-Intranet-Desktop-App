using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BookstoreApp.Helpers;
using BookstoreApp.Models;
using BookstoreApp.Models.Contexts;

namespace BookstoreApp.ViewModels.Single
{
    public class AddAuthorViewModel : WorkspaceViewModel, INotifyPropertyChanged
    {
        private string _topLabel;
        private string _firstName;
        private string _lastName;
        private bool _isAlive;
        private string _selectedCountry;
        private string _errorLabel;
        public ObservableCollection<string> Countries { get; }

        public AddAuthorViewModel() : base("Add Author")
        {
            AddAuthorCommand = new BaseCommand(() => AddNewAuthor());
            TopLabel = "Add new author";
            ErrorLabel = "";
            IsAlive = true; // Default value
            Countries = new ObservableCollection<string> { "Poland", "Germany", "USA" };
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

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public bool IsAlive
        {
            get => _isAlive;
            set
            {
                _isAlive = value;
                OnPropertyChanged(nameof(IsAlive));
            }
        }

        public string SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                _selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
            }
        }

        public string ErrorLabel
        {
            get => _errorLabel;
            set
            {
                _errorLabel = value;
                OnPropertyChanged(nameof(ErrorLabel));
            }
        }

        public ICommand AddAuthorCommand { get; }

        private void AddNewAuthor()
        {
            if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName) && !string.IsNullOrWhiteSpace(SelectedCountry))
            {
                DatabaseContext db = new DatabaseContext();
                
                Author author = new Author
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    CountryOrigin = SelectedCountry,
                    IsAlive = IsAlive,
                    CreationDateTime = DateTime.Now,
                    IsActive = true
                };
                db.Authors.Add(author);
                db.SaveChanges();
                TopLabel = $"Author {author.FirstName} {author.LastName} successfully added.";
                ErrorLabel = "";
                
            }
            else
            {
                ErrorLabel = "Unable to add a new author. Please fill in all fields.";
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
