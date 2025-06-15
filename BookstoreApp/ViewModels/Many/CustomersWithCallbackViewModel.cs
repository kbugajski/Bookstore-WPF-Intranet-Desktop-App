using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Helpers;
using BookstoreApp.Models.Dtos;
using CommunityToolkit.Mvvm.Messaging;

namespace BookstoreApp.ViewModels.Many
{
    public class CustomersWithCallbackViewModel : CustomersViewModel
    {
        public object WhoRequsetedToSelect { get; set; }
        public CustomersWithCallbackViewModel(object whoRequestedToSelect) 
        {
            WhoRequsetedToSelect = whoRequestedToSelect;
        }

        protected override void HandleSelect()
        {
            WeakReferenceMessenger.Default.Send<SelectedObjectMessage<CustomerDto>>(new SelectedObjectMessage<CustomerDto>(WhoRequsetedToSelect, SelectedModel!));
            OnRequestClose();
        }
    }
}
