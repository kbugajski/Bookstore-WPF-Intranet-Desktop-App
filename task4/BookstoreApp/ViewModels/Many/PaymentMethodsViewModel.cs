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
    public class PaymentMethodsViewModel : WorkspaceViewModel
    {
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        protected DatabaseContext Database { get; private set; }
        private ObservableCollection<PaymentMethod> _Models;
        public ObservableCollection<PaymentMethod> Models
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

        private string searchInput { get; set; }
        public string SearchInput
        {
            get { return searchInput; }
            set
            {
                if (searchInput != value)
                {
                    searchInput = value;
                    OnPropertyChanged(() => SearchInput);
                    Initialize();
                }

            }
        }

        private PaymentMethod? _SelectedModel;
        public PaymentMethod? SelectedModel
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
        public PaymentMethodsViewModel() : base("Payment Methods")
        {

            RefreshCommand = new BaseCommand(() => Initialize());
            DeleteCommand = new BaseCommand(() => Delete());
            Initialize();
        }

        private void Initialize()
        {
            Database = new DatabaseContext();
            IQueryable<PaymentMethod> paymentMethods = Database.PaymentMethods.Where(x => x.IsActive);  

            if (!string.IsNullOrEmpty(searchInput))
            {
                paymentMethods = paymentMethods.Where(item => item.Title.Contains(searchInput));


            }
            Models = new ObservableCollection<PaymentMethod>(paymentMethods.ToList());
        }

        public void Delete()
        {
            if (_Models != null)
            {
                if (SelectedModel != null)
                {
                    PaymentMethod? paymentMethod = Database.PaymentMethods.Find(SelectedModel.Id);
                    if (paymentMethod != null)
                    {
                        paymentMethod.IsActive = false;
                        paymentMethod.DeleteDateTime = DateTime.Now;
                        Database.SaveChanges();
                        Models.Remove(paymentMethod);
                    }
                }

            }
        }
    }
}
