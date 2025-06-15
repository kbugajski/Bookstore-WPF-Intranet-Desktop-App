using System;
using System.Windows.Input;
using BookstoreApp.Helpers;
using BookstoreApp.Models.Dtos;
using BookstoreApp.Models.Services;
using BookstoreApp.ViewModels.Many;
using CommunityToolkit.Mvvm.Messaging;

namespace BookstoreApp.ViewModels
{
    public class DashboardViewModel : WorkspaceViewModel
    {
        public DashboardService Service { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand SelectBookCommand { get; set; }

        public int TotalBooks
        {
            get => Service.TotalBooks;
            set
            {
                if (value != Service.TotalBooks)
                {
                    Service.TotalBooks = value;
                    OnPropertyChanged(() => TotalBooks);
                }
            }
        }

        public int TotalCustomers
        {
            get => Service.TotalCustomers;
            set
            {
                if (value != Service.TotalCustomers)
                {
                    Service.TotalCustomers = value;
                    OnPropertyChanged(() => TotalCustomers);
                }
            }
        }

        public int TotalInvoices
        {
            get => Service.TotalInvoices;
            set
            {
                if (value != Service.TotalInvoices)
                {
                    Service.TotalInvoices = value;
                    OnPropertyChanged(() => TotalInvoices);
                }
            }
        }

        public int TotalAuthors
        {
            get => Service.TotalAuthors;
            set
            {
                if (value != Service.TotalAuthors)
                {
                    Service.TotalAuthors = value;
                    OnPropertyChanged(() => TotalAuthors);
                }
            }
        }

        public int TotalPublishers
        {
            get => Service.TotalPublishers;
            set
            {
                if (value != Service.TotalPublishers)
                {
                    Service.TotalPublishers = value;
                    OnPropertyChanged(() => TotalPublishers);
                }
            }
        }

        public int TotalReviews
        {
            get => Service.TotalReviews;
            set
            {
                if (value != Service.TotalReviews)
                {
                    Service.TotalReviews = value;
                    OnPropertyChanged(() => TotalReviews);
                }
            }
        }

        public int TotalCategories
        {
            get => Service.TotalCategories;
            set
            {
                if (value != Service.TotalCategories)
                {
                    Service.TotalCategories = value;
                    OnPropertyChanged(() => TotalCategories);
                }
            }
        }

        public int TotalAddresses
        {
            get => Service.TotalAddresses;
            set
            {
                if (value != Service.TotalAddresses)
                {
                    Service.TotalAddresses = value;
                    OnPropertyChanged(() => TotalAddresses);
                }
            }
        }

        public string ReviewRatingAverage
        {
            get => Service.ReviewRatingAverage;
            set
            {
                if (value != Service.ReviewRatingAverage)
                {
                    Service.ReviewRatingAverage = value;
                    OnPropertyChanged(() => ReviewRatingAverage);
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

        public int SelectedBookId
        {
            get => Service.SelectedBookId;
            set
            {
                if (Service.SelectedBookId != value)
                {
                    Service.SelectedBookId = value;
                    OnPropertyChanged(() => SelectedBookId);
                }
            }
        }

        // Nowe właściwości dla wybranej książki
        public int SelectedBookTotalAuthors
        {
            get => Service.SelectedBookTotalAuthors;
            set
            {
                if (value != Service.SelectedBookTotalAuthors)
                {
                    Service.SelectedBookTotalAuthors = value;
                    OnPropertyChanged(() => SelectedBookTotalAuthors);
                }
            }
        }

        public int SelectedBookTotalCategories
        {
            get => Service.SelectedBookTotalCategories;
            set
            {
                if (value != Service.SelectedBookTotalCategories)
                {
                    Service.SelectedBookTotalCategories = value;
                    OnPropertyChanged(() => SelectedBookTotalCategories);
                }
            }
        }

        public int SelectedBookTotalPublishers
        {
            get => Service.SelectedBookTotalPublishers;
            set
            {
                if (value != Service.SelectedBookTotalPublishers)
                {
                    Service.SelectedBookTotalPublishers = value;
                    OnPropertyChanged(() => SelectedBookTotalPublishers);
                }
            }
        }

        public string SelectedBookReviewRatingAverage
        {
            get => Service.SelectedBookReviewRatingAverage;
            set
            {
                if (value != Service.SelectedBookReviewRatingAverage)
                {
                    Service.SelectedBookReviewRatingAverage = value;
                    OnPropertyChanged(() => SelectedBookReviewRatingAverage);
                }
            }
        }

        public string SelectedBookReviewRatingLargest
        {
            get => Service.SelectedBookReviewRatingLargest;
            set
            {
                if (value != Service.SelectedBookReviewRatingLargest)
                {
                    Service.SelectedBookReviewRatingLargest = value;
                    OnPropertyChanged(() => SelectedBookReviewRatingLargest);
                }
            }
        }

        public string SelectedBookReviewRatingSmallest
        {
            get => Service.SelectedBookReviewRatingSmallest;
            set
            {
                if (value != Service.SelectedBookReviewRatingSmallest)
                {
                    Service.SelectedBookReviewRatingSmallest = value;
                    OnPropertyChanged(() => SelectedBookReviewRatingSmallest);
                }
            }
        }

        public DashboardViewModel() : base("Dashboard")
        {
            BookTitle = "Select Book";
            Service = new DashboardService();
            RefreshCommand = new BaseCommand(() => Refresh());
            SelectBookCommand = new BaseCommand(() => SelectBook());
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<BookDto>>(this, (recipient, message) => GetSelectedBook(message));
            Refresh();
        }

        public void Refresh()
        {
            Service.Refresh();
        }

        private void SelectBook()
        {
            WindowManager.OpenWindow(new BooksWithCallbackViewModel(this));
        }

        private void GetSelectedBook(SelectedObjectMessage<BookDto> message)
        {
            if (message.WhoRequestedToSelect == this)
            {
                SelectedBookId = message.SelectedObject.Id;
                BookTitle = message.SelectedObject.Title;
                Service.InitializeSelectedBook(SelectedBookId);

                // Upewnienie się, że dane w ViewModel zostaną odświeżone
                OnPropertyChanged(() => SelectedBookTotalAuthors);
                OnPropertyChanged(() => SelectedBookTotalCategories);
                OnPropertyChanged(() => SelectedBookTotalPublishers);
                OnPropertyChanged(() => SelectedBookReviewRatingAverage);
                OnPropertyChanged(() => SelectedBookReviewRatingLargest);
                OnPropertyChanged(() => SelectedBookReviewRatingSmallest);
            }
        }
    }
}
