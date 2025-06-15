using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Helpers;
using BookstoreApp.Models.Contexts;
using System.Windows.Input;
using BookstoreApp.Models;

namespace BookstoreApp.ViewModels.Many
{
    public class InvoicesViewModel : WorkspaceViewModel
    {
        public InvoicesViewModel() : base("Invoices")
        {
            RefreshCommand = new BaseCommand(() => Initialize());
            DeleteCommand = new BaseCommand(() => Delete());
            Initialize();
        }
        
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        protected DatabaseContext Database { get; private set; }
        private ObservableCollection<Invoice> _Models;
        public ObservableCollection<Invoice> Models
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

        private Invoice? _SelectedModel;
        public Invoice? SelectedModel
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

        private void Initialize()
        {
            Database = new DatabaseContext();
            List<Invoice> invoice = Database.Invoices.Where(x => x.IsActive).ToList();
            Models = new ObservableCollection<Invoice>(invoice);
        }

        public void Delete()
        {
            if (_Models != null)
            {
                if (SelectedModel != null)
                {
                    Invoice? invoice = Database.Invoices.Find(SelectedModel.Id);
                    if (invoice != null)
                    {
                        invoice.IsActive = false;
                        invoice.DeleteDateTime = DateTime.Now;
                        Database.SaveChanges();
                        Models.Remove(invoice);
                    }
                }

            }
        }
    }
}

