using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookstoreApp.Helpers;
using BookstoreApp.Models;
using BookstoreApp.Models.Dtos;
using BookstoreApp.Models.Services;
using BookstoreApp.ViewModels.Many;
using CommunityToolkit.Mvvm.Messaging;

namespace BookstoreApp.ViewModels.Single
{
    public class AddReviewViewModel : BaseCreateViewModel<ReviewService, ReviewDto, Review>
    {
        public ICommand SelectCustomerCommand { get; set; }
        public ICommand SelectBookCommand { get; set; }
        public int AuthorId
        {
            get => Model.CustomerId;
            set
            {
                if (Model.CustomerId != value)
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
                if (Model.BookId != value)
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
                if (Model?.ReviewDate != value)
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
                if (Model.Rating != value)
                {
                    Model.Rating = value;
                    OnPropertyChanged(() => Rating);
                }
            }
        }

        private string _AuthorTitle;
        public string AuthorTitle
        {
            get => _AuthorTitle;
            set
            {
                if (value != _AuthorTitle)
                {
                    _AuthorTitle = value;
                    OnPropertyChanged(() => AuthorTitle);
                }
            }
        }

        private string _BookTitle;
        public string BookTitle
        {
            get => _BookTitle;
            set
            {
                if (value != _BookTitle)
                {
                    _BookTitle = value;
                    OnPropertyChanged(() => BookTitle);
                }
            }
        }

        public AddReviewViewModel(Review model) : base("Edit review", model)
        {
            TopLabel = "Edit review";
            if (ReviewDate == DateTime.MinValue)
            {
                ReviewDate = DateTime.Now;
            }
            SelectCustomerCommand = new BaseCommand(() => SelectCustomer());
            SelectBookCommand = new BaseCommand(() => SelectBook());
            AuthorTitle = Model.Customer.FirstName.Trim() + " " + Model.Customer.LastName.Trim();
            BookTitle = Model.Book.Title;
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<CustomerDto>>(this, (recipient, message) => GetSelectedCustomer(message));
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<BookDto>>(this, (recipient, message) => GetSelectedBook(message));
        }

        public AddReviewViewModel() : base("Add Review")
        {
            TopLabel = "Add new review";
            if (ReviewDate == DateTime.MinValue)
            {
                ReviewDate = DateTime.Now;
            }
            SelectCustomerCommand = new BaseCommand(() => SelectCustomer());
            SelectBookCommand = new BaseCommand(() => SelectBook());
            AuthorTitle = "Select customer";
            BookTitle = "Select book";
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<CustomerDto>>(this, (recipient, message) => GetSelectedCustomer(message));
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<BookDto>>(this, (recipient, message) => GetSelectedBook(message));
        }

        private void SelectCustomer()
        {
            //WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            //{
            //    Sender = this,
            //    ViewModelToBeOpened = new CountriesWithCallbackViewModel(this)
            //});
            WindowManager.OpenWindow(new CustomersWithCallbackViewModel(this));
        }

        private void GetSelectedCustomer(SelectedObjectMessage<CustomerDto> message)
        {
            if (message.WhoRequestedToSelect == this)
            {
                AuthorId = message.SelectedObject.Id;
                AuthorTitle = message.SelectedObject.FirstName.Trim() + " " + message.SelectedObject.LastName.Trim();
            }
        }

        private void SelectBook()
        {
            //WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            //{
            //    Sender = this,
            //    ViewModelToBeOpened = new CountriesWithCallbackViewModel(this)
            //});
            WindowManager.OpenWindow(new BooksWithCallbackViewModel(this));
        }

        private void GetSelectedBook(SelectedObjectMessage<BookDto> message)
        {
            if (message.WhoRequestedToSelect == this)
            {
                BookId = message.SelectedObject.Id;
                BookTitle = message.SelectedObject.Title;
            }
        }
    }
}
