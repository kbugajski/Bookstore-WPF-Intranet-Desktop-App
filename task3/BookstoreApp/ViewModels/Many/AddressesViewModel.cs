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
    public class AddressesViewModel : WorkspaceViewModel
    {
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        protected DatabaseContext Database { get; private set; }
        private ObservableCollection<Address> _Models;
        public ObservableCollection<Address> Models
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

        private Address? _SelectedModel;
        public Address? SelectedModel
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
        public AddressesViewModel() : base("Address")
        {

            RefreshCommand = new BaseCommand(() => Initialize());
            DeleteCommand = new BaseCommand(() => Delete());
            Initialize();
        }

        private void Initialize()
        {
            Database = new DatabaseContext();
            List<Address> addresses = Database.Addresses.Where(x => x.IsActive).ToList();
            Models = new ObservableCollection<Address>(addresses);
        }

        public void Delete()
        {
            if (_Models != null)
            {
                if (SelectedModel != null)
                {
                    Address? addresses = Database.Addresses.Find(SelectedModel.Id);
                    if (addresses != null)
                    {
                        addresses.IsActive = false;
                        addresses.DeleteDateTime = DateTime.Now;
                        Database.SaveChanges();
                        Models.Remove(addresses);
                    }
                }

            }
        }
    }
}

