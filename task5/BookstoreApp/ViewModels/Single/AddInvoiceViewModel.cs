using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookstoreApp.Helpers;
using BookstoreApp.Models;
using BookstoreApp.Models.Dtos;
using BookstoreApp.Models.Services;
using BookstoreApp.ViewModels.Many;
using CommunityToolkit.Mvvm.Messaging;

namespace BookstoreApp.ViewModels.Single
{
    public class AddInvoiceViewModel : BaseCreateViewModel<InvoiceService, InvoiceDto, Invoice>
    {
        public ICommand SelectCustomerCommand { get; set; }
        public string Number
        {
            get => Model.Number;
            set
            {
                if(Model.Number != value)
                {
                    Model.Number = value;
                    OnPropertyChanged(() => Number);
                }
            }
        }

        public DateTime DateOfIssue
        {
            get => Model.DateOfIssue;
            set
            {
                if (Model.DateOfIssue != value)
                {
                    Model.DateOfIssue = value;
                    OnPropertyChanged(() => DateOfIssue);
                }
            }

        }
        public int CustomerId
        {
            get => Model.CustomerId;
            set
            {
                if (Model.CustomerId != value)
                {
                    Model.CustomerId = value;
                    OnPropertyChanged(() => CustomerId);
                }
            }
        }


        public int PaymentMethodId
        {
            get => Model.PaymentMethodId;
            set
            {
                if (!Model.PaymentMethodId.Equals(value))
                {
                    Model.PaymentMethodId = value;
                    OnPropertyChanged(() => PaymentMethodId);
                }
            }
        }

        public bool IsPaid
        {
            get => Model.IsPaid;
            set
            {
                if(Model.IsPaid != value)
                {
                    Model.IsPaid = value;
                    OnPropertyChanged(() => IsPaid);
                }
            }
        }
        public DateTime PaymentDate
        {
            get => Model.PaymentDate;
            set
            {
                if (!Model.PaymentDate.Equals(value))
                {
                    Model.PaymentDate = value;
                    OnPropertyChanged(() => PaymentDate);
                }
            }
        }

        private string _CustomerTitle;
        public string CustomerTitle
        {
            get => _CustomerTitle;
            set
            {
                if (value != _CustomerTitle)
                {
                    _CustomerTitle = value;
                    OnPropertyChanged(() => CustomerTitle);
                }
            }
        }
        public AddInvoiceViewModel() : base("Add Invoice")
        {
            TopLabel = "Add Invoice";
            if(DateOfIssue == DateTime.MinValue)
            {
                DateOfIssue = DateTime.Now;
            }
            if (PaymentDate == DateTime.MinValue)
            {
                PaymentDate = DateTime.Now;
            }
            SelectCustomerCommand = new BaseCommand(() => SelectCustomer());
            CustomerTitle = "Select Customer";
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<CustomerDto>>(this, (recipient, message) => GetSelectedCustomer(message));
        }

        public AddInvoiceViewModel(Invoice model) : base("Edit Invoice", model)
        {
            TopLabel = "Edit Inovice";
            if (DateOfIssue == DateTime.MinValue)
            {
                DateOfIssue = DateTime.Now;
            }
            if (PaymentDate == DateTime.MinValue)
            {
                PaymentDate = DateTime.Now;
            }
            SelectCustomerCommand = new BaseCommand(() => SelectCustomer());
            CustomerTitle = Model.Customer.FirstName.Trim() + " " + Model.Customer.LastName.Trim();
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<CustomerDto>>(this, (recipient, message) => GetSelectedCustomer(message));
        }

        private void SelectCustomer()
        {
            //WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            //{
            //    Sender = this,
            //    ViewModelToBeOpened = new CountriesWithCallbackViewModel(this)
            //});
            WindowManager.OpenWindow(new CustomersWithCallbackViewModel(this));
        }

        private void GetSelectedCustomer(SelectedObjectMessage<CustomerDto> message)
        {
            if (message.WhoRequestedToSelect == this)
            {
                CustomerId = message.SelectedObject.Id;
                CustomerTitle = message.SelectedObject.FirstName.Trim() + " " + message.SelectedObject.LastName.Trim();
            }
        }
    }
}
