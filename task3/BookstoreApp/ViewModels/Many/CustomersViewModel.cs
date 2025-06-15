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
    public class CustomersViewModel : WorkspaceViewModel
    {
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        protected DatabaseContext Database { get; private set; }
        private ObservableCollection<Customer> _Models;
        public ObservableCollection<Customer> Models
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

        private Customer? _SelectedModel;
        public Customer? SelectedModel
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
        public CustomersViewModel() : base("Customers")
        {

            RefreshCommand = new BaseCommand(() => Initialize());
            DeleteCommand = new BaseCommand(() => Delete());
            Initialize();
        }

        private void Initialize()
        {
            Database = new DatabaseContext();
            List<Customer> customers = Database.Customers.Where(x => x.IsActive).ToList();
            Models = new ObservableCollection<Customer>(customers);
        }

        public void Delete()
        {
            if (_Models != null)
            {
                if (SelectedModel != null)
                {
                    Customer? customer = Database.Customers.Find(SelectedModel.Id);
                    if (customer != null)
                    {
                        customer.IsActive = false;
                        customer.DeleteDateTime = DateTime.Now;
                        Database.SaveChanges();
                        Models.Remove(customer);
                    }
                }

            }
        }
    }
}
