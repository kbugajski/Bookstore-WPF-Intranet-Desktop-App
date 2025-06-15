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
using BookstoreApp.ViewModels.Single;
using CommunityToolkit.Mvvm.Messaging;
using BookstoreApp.Models.Services;
using BookstoreApp.Models.Dtos;

namespace BookstoreApp.ViewModels.Many
{
    public class AuthorsViewModel : BaseManyViewModel<AuthorService,AuthorDto, Author>
    {
        public bool IsAliveCheck
        {
            get => Service.IsAliveCheck;
            set
            {
                if(Service.IsAliveCheck != value)
                {
                    Service.IsAliveCheck = value;
                    OnPropertyChanged(() => IsAliveCheck);
                }
            }
        }
        public YesNoEnum HasBiography
        {
            get => Service.HasBiography;
            set
            {
                if(Service.HasBiography != value)
                {
                    Service.HasBiography = value;
                    OnPropertyChanged(() => HasBiography);
                }
            }
        }
        public AuthorsViewModel() : base("Authors")
        {
        }

        protected override void ClearFilter()
        {
            IsAliveCheck = false;
            HasBiography = YesNoEnum.NoFilter;
            SearchProperty = SearchComboBoxDto.FirstOrDefault()?.PropertyTitle;
            SearchInput = string.Empty;
            OrderAscending = false;
            OrderProperty = OrderByComboBoxDto?.FirstOrDefault()?.PropertyTitle;
            Refresh();
        }

        protected override void CreateNew()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new AddAuthorViewModel()
            });
        }

        protected override void Edit()
        {
            if (SelectedModel != null)
            {
                Author modelToEdit = Service.GetModel(SelectedModel.Id);
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
                {
                    Sender = this,
                    ViewModelToBeOpened = new AddAuthorViewModel(modelToEdit)
                });
            }
        }
    }
}

