using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookstoreApp.Helpers;
using BookstoreApp.Models;
using BookstoreApp.Models.Contexts;
using BookstoreApp.Models.Dtos;
using BookstoreApp.Models.Services;
using BookstoreApp.ViewModels.Single;
using CommunityToolkit.Mvvm.Messaging;

namespace BookstoreApp.ViewModels.Many
{
    public class ReviewsViewModel : BaseManyViewModel<ReviewService, ReviewDto, Review>
    {
        public double Rating
        {
            get => Service.Rating;
            set
            {
                if(Service.Rating != value)
                {
                    Service.Rating = value;
                    OnPropertyChanged(() =>  Rating);
                }
            }
        }
        public YesNoEnum HasReviewText
        {
            get => Service.HasReviewText;
            set
            {
                if (Service.HasReviewText != value)
                {
                    Service.HasReviewText = value;
                    OnPropertyChanged(() => HasReviewText);
                }
            }
        }
        public LargerSmallerEnum SelectedLargerSmallerOption
        {
            get => Service.SelectedLargerSmallerOption;
            set
            {
                if(Service.SelectedLargerSmallerOption != value)
                {
                    Service.SelectedLargerSmallerOption = value;
                    OnPropertyChanged(() => SelectedLargerSmallerOption);
                }
            }
        }

        public ReviewsViewModel() : base("Reivews")
        { 
        }

        protected override void ClearFilter()
        {
            HasReviewText = YesNoEnum.NoFilter;
            SearchProperty = SearchComboBoxDto.FirstOrDefault()?.PropertyTitle;
            SearchInput = string.Empty;
            Rating = 1;
            OrderProperty = OrderByComboBoxDto.FirstOrDefault()?.PropertyTitle;
            OrderAscending = false;
            Refresh();
        }

        protected override void CreateNew()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new AddReviewViewModel()
            });
        }

        protected override void Edit()
        {
            if (SelectedModel != null)
            {
                Review modelToEdit = Service.GetModel(SelectedModel.Id);
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
                {
                    Sender = this,
                    ViewModelToBeOpened = new AddReviewViewModel(modelToEdit)
                });
            }
        }
    }
}

