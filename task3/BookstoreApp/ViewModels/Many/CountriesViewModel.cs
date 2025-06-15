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

namespace BookstoreApp.ViewModels.Many
{
    public class CountriesViewModel : WorkspaceViewModel
    {
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        protected DatabaseContext Database { get; private set; }
        private ObservableCollection<Country> _Models;
        public ObservableCollection<Country> Models
        {
            get => _Models;
            set
            {
                if (value != _Models)
                {
                    _Models = value;
                    OnPropertyChanged(() => Models);
                }
            }
        }

        private Country? _SelectedModel;
        public Country? SelectedModel
        {
            get => _SelectedModel;
            set
            {
                if (value != _SelectedModel)
                {
                    _SelectedModel = value;
                    OnPropertyChanged(() => SelectedModel);
                }
            }
        }
        public CountriesViewModel() : base("Countries")
        {

            RefreshCommand = new BaseCommand(() => Initialize());
            DeleteCommand = new BaseCommand(() => Delete());
            Initialize();
        }

        private void Initialize()
        {
            Database = new DatabaseContext();
            List<Country> countries = Database.Countries.Where(x => x.IsActive).ToList();
            Models = new ObservableCollection<Country>(countries);
        }

        public void Delete()
        {
            if (_Models != null)
            {
                if (SelectedModel != null)
                {
                    Country? country = Database.Countries.Find(SelectedModel.Id);
                    if (country != null)
                    {
                        country.IsActive = false;
                        country.DeleteDateTime = DateTime.Now;
                        Database.SaveChanges();
                        Models.Remove(country);
                    }
                }

            }
        }
    }
}
