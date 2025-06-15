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
using System.Security.Policy;
using Publisher = BookstoreApp.Models.Publisher;
using BookstoreApp.Models.Services;
using BookstoreApp.Models.Dtos;
using BookstoreApp.ViewModels.Single;
using CommunityToolkit.Mvvm.Messaging;

namespace BookstoreApp.ViewModels.Many
{
    public class PublishersViewModel : BaseManyViewModel<PublisherService, PublisherDto, Publisher>
    {
        public PublishersViewModel() : base("Publishers")
        {
            HasAddress = YesNoEnum.NoFilter;
            HasNip = YesNoEnum.NoFilter;
        }


        public YesNoEnum HasAddress
        {
            get => Service.HasAddress;
            set
            {
                if (Service.HasAddress != value)
                {
                    Service.HasAddress = value;
                    OnPropertyChanged(() => HasAddress);
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


        protected override void ClearFilter()
        {
            throw new NotImplementedException();
        }

        protected override void CreateNew()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new AddPublisherViewModel()
            });
        }
    }
}
