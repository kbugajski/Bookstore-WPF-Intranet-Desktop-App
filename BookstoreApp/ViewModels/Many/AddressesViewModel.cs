﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Helpers;
using BookstoreApp.Models.Contexts;
using BookstoreApp.Models;
using System.Windows.Input;
using BookstoreApp.ViewModels.Single;
using CommunityToolkit.Mvvm.Messaging;
using BookstoreApp.Models.Services;
using BookstoreApp.Models.Dtos;

namespace BookstoreApp.ViewModels.Many
{
    public class AddressesViewModel : BaseManyViewModel<AddressService, AddressDto, Address>
    {
        public YesNoEnum HasPhoneNumber 
        {
            get => Service.HasPhoneNumber;
            set
            {
                if(Service.HasPhoneNumber != value)
                {
                    Service.HasPhoneNumber = value;
                    OnPropertyChanged(() => HasPhoneNumber);
                }
            }
        }


        public AddressesViewModel() : base("Addresses")
        {
        }

       

        protected override void ClearFilter()
        {
            HasPhoneNumber = YesNoEnum.NoFilter;
            SearchInput = string.Empty;
            SearchProperty = SearchComboBoxDto.FirstOrDefault()?.PropertyTitle;
            OrderAscending = false;
            OrderProperty = OrderByComboBoxDto?.FirstOrDefault()?.PropertyTitle;
            Refresh();
        }

        protected override void CreateNew()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new AddAddressViewModel()
            });
        }

        protected override void Edit()
        {
            if (SelectedModel != null)
            {
                Address modelToEdit = Service.GetModel(SelectedModel.Id);
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
                {
                    Sender = this,
                    ViewModelToBeOpened = new AddAddressViewModel(modelToEdit)
                });
            }
        }
        protected override void HandleSelect()
        {
            
            
        }

    }
}

