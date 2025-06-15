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
    public class CountriesWithCallbackViewModel : CountriesViewModel 
    {
        public object WhoRequsetedToSelect { get; set; }

        public CountriesWithCallbackViewModel(object whoRequsetedToSelect)
        {
            WhoRequsetedToSelect = whoRequsetedToSelect;
        }


        protected override void HandleSelect()
        {
            WeakReferenceMessenger.Default.Send<SelectedObjectMessage<CountryDto>>(new SelectedObjectMessage<CountryDto>(WhoRequsetedToSelect, SelectedModel!));
            OnRequestClose();
        }
    }
}
