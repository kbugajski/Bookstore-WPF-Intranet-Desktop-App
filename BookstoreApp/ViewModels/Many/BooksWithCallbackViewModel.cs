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
    public class BooksWithCallbackViewModel : BooksViewModel
    {
        public object WhoRequsetedToSelect { get; set; }
        public BooksWithCallbackViewModel(object whoRequestedToSelect)
        {
            WhoRequsetedToSelect = whoRequestedToSelect;
        }

        protected override void HandleSelect()
        {
            WeakReferenceMessenger.Default.Send<SelectedObjectMessage<BookDto>>(new SelectedObjectMessage<BookDto>(WhoRequsetedToSelect, SelectedModel!));
            OnRequestClose();
        }
    }
}
