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
using BookstoreApp.Models.Services;
using BookstoreApp.Models.Dtos;
using BookstoreApp.ViewModels.Single;
using CommunityToolkit.Mvvm.Messaging;

namespace BookstoreApp.ViewModels.Many
{
    public class InvoicesViewModel : BaseManyViewModel<InvoiceService, InvoiceDto, Invoice>
    {
        public InvoicesViewModel() : base("Invoices")
        {
        }

        protected override void ClearFilter()
        {
            IsPaid = false;
            SearchInput = string.Empty;
            SearchProperty = SearchComboBoxDto.FirstOrDefault()?.PropertyTitle;
            DateOfIssueFrom = null;
            DateOfIssueTo = null;
            OrderProperty = OrderByComboBoxDto.FirstOrDefault()?.PropertyTitle;
            OrderAscending = false;
            Refresh();
        }
        public bool IsPaid
        {
            get => Service.IsPaid;
            set
            {
                if (Service.IsPaid != value)
                {
                    Service.IsPaid = value;
                    OnPropertyChanged(() => IsPaid);
                }
            }
        }
        public DateTime? DateOfIssueFrom
        {
            get => Service.DateOfIssueFrom;
            set
            {
                if(Service.DateOfIssueFrom != value)
                {
                    Service.DateOfIssueFrom = value;
                    OnPropertyChanged(() => DateOfIssueFrom);
                }
            }
        }
        public DateTime? DateOfIssueTo
        {
            get => Service.DateOfIssueTo;
            set
            {
                if (Service.DateOfIssueTo != value)
                {
                    Service.DateOfIssueTo = value;
                    OnPropertyChanged(() => DateOfIssueTo);
                }
            }
        }



        protected override void CreateNew()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new AddInvoiceViewModel()
            });
        }

        protected override void Edit()
        {
            if (SelectedModel != null)
            {
                Invoice modelToEdit = Service.GetModel(SelectedModel.Id);
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
                {
                    Sender = this,
                    ViewModelToBeOpened = new AddInvoiceViewModel(modelToEdit)
                });
            }
        }
    }
}

