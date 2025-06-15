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
        public ReviewsViewModel() : base("Reivews")
        {
            Rating = 1;
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
                ViewModelToBeOpened = new AddReviewViewModel()
            });
        }
    }
}

