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
using BookstoreApp.Models.Dtos;
using BookstoreApp.Models.Services;
using CommunityToolkit.Mvvm.Messaging;
using BookstoreApp.ViewModels.Single;
using static BookstoreApp.ViewModels.Single.AddCustomerViewModel;

namespace BookstoreApp.ViewModels.Many
{
    public class CustomersViewModel : BaseManyViewModel<CustomerService, CustomerDto, Customer>
    {
        public YesNoEnum HasCode
        {
            get => Service.HasCode;
            set
            {
                if (Service.HasCode != value)
                {
                    Service.HasCode = value;
                    OnPropertyChanged(() => HasCode);
                }
            }
        }

        public YesNoEnum HasNip
        {
            get => Service.HasNip;
            set
            {
                if (Service.HasNip != value)
                {
                    Service.HasNip = value;
                    OnPropertyChanged(() => HasNip);
                }
            }
        }


        public CustomersViewModel() : base("Customers")
        {

        }

        protected override void ClearFilter()
        {
            HasCode = YesNoEnum.NoFilter;
            HasNip = YesNoEnum.NoFilter;
            SearchInput = string.Empty;
            SearchProperty = SearchComboBoxDto?.FirstOrDefault()?.PropertyTitle;
            OrderAscending = false;
            OrderProperty = OrderByComboBoxDto?.FirstOrDefault()?.PropertyTitle;
            Refresh();
        }

        protected override void HandleSelect()
        { 
        }

        protected override void CreateNew()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new AddCustomerViewModel()
            });
        }

        protected override void Edit()
        {
            if (SelectedModel != null)
            {
                Customer modelToEdit = Service.GetModel(SelectedModel.Id);
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
                {
                    Sender = this,
                    ViewModelToBeOpened = new AddCustomerViewModel(modelToEdit)
                });
            }
        }
    }
}
