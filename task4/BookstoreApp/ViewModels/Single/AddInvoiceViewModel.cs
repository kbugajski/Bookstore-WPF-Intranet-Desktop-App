using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Models;
using BookstoreApp.Models.Dtos;
using BookstoreApp.Models.Services;

namespace BookstoreApp.ViewModels.Single
{
    public class AddInvoiceViewModel : BaseCreateViewModel<InvoiceService, InvoiceDto, Invoice>
    {
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
                if (!Model.CustomerId.Equals(value))
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
        public AddInvoiceViewModel() : base("Add Invoice")
        {
            if(DateOfIssue == DateTime.MinValue)
            {
                DateOfIssue = DateTime.Now;
            }
            if (PaymentDate == DateTime.MinValue)
            {
                PaymentDate = DateTime.Now;
            }
        }
    }
}
