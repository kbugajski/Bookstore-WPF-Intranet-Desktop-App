using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Models;
using BookstoreApp.Models.Dtos;
using BookstoreApp.Models.Services;

namespace BookstoreApp.ViewModels.Single
{
    public class AddReviewViewModel : BaseCreateViewModel<ReviewService, ReviewDto, Review>
    {
        public int AuthorId 
        {
            get => Model.CustomerId;
            set
            {
                if(Model.CustomerId != value)
                {
                    Model.CustomerId = value;
                    OnPropertyChanged(() => AuthorId);
                }
            }
        }

        public int BookId
        {
            get => Model.BookId;
            set
            {
                if(Model.BookId != value)
                {
                    Model.BookId = value;
                    OnPropertyChanged(() => BookId);
                }
            }
        }
        public string ReviewText
        {
            get => Model.ReviewText;
            set
            {
                if (Model.ReviewText != value)
                {
                    Model.ReviewText = value;
                    OnPropertyChanged(() => ReviewText);
                }
            }
        }
        public DateTime ReviewDate
        {
            get => Model.ReviewDate;
            set
            {
                if(Model?.ReviewDate != value)
                {
                    Model.ReviewDate = value;
                    OnPropertyChanged(() => ReviewDate);
                }
            }
        }
        public double Rating
        {
            get => Model.Rating;
            set
            {
                if(Model.Rating != value)
                {
                    Model.Rating = value;
                    OnPropertyChanged(() => Rating);
                }
            }
        }


        public AddReviewViewModel() : base("Add Review")
        {
            if(ReviewDate == DateTime.MinValue)
            {
                ReviewDate = DateTime.Now;
            }
        }
    }

}
