using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Models.Contexts;

namespace BookstoreApp.Models.Services
{
    public class DashboardService
    {
        protected DatabaseContext DatabaseContext { get; set; }

        public int TotalBooks { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalInvoices { get; set; }
        public int TotalAuthors { get; set; }
        public int TotalPublishers { get; set; }
        public int TotalReviews { get; set; }
        public int TotalCategories { get; set; }
        public int TotalAddresses { get; set; }
        public string ReviewRatingAverage { get; set; }

        public int SelectedBookId { get; set; }
        public int SelectedBookTotalAuthors { get; set; }
        public int SelectedBookTotalCategories { get; set; }
        public int SelectedBookTotalPublishers { get; set; }
        public string SelectedBookReviewRatingAverage { get; set; }
        public string SelectedBookReviewRatingLargest { get; set; }
        public string SelectedBookReviewRatingSmallest { get; set; }

        public DashboardService()
        {
            DatabaseContext = new DatabaseContext();
        }

        public void Refresh()
        {
            TotalBooks = countBooks();
            TotalCustomers = countCustomers();
            TotalInvoices = countInvoices();
            TotalAuthors = countAuthors();
            TotalPublishers = countPublishers();
            TotalReviews = countReviews();
            TotalCategories = countCategories();
            TotalAddresses = countAddresses();
            ReviewRatingAverage = calculateAverageReviewRating();
        }

        public int countBooks() => DatabaseContext.Books.Where(item => item.IsActive).Count();
        public int countCustomers() => DatabaseContext.Customers.Where(item => item.IsActive).Count();
        public int countInvoices() => DatabaseContext.Invoices.Where(item => item.IsActive).Count();
        public int countAuthors() => DatabaseContext.Authors.Where(item => item.IsActive).Count();
        public int countPublishers() => DatabaseContext.Publishers.Where(item => item.IsActive).Count();
        public int countReviews() => DatabaseContext.Reviews.Where(item => item.IsActive).Count();
        public int countCategories() => DatabaseContext.Categories.Where(item => item.IsActive).Count();
        public int countAddresses() => DatabaseContext.Addresses.Where(item => item.IsActive).Count();

        public string calculateAverageReviewRating()
        {
            return DatabaseContext.Reviews.Any(item => item.IsActive)
                ? DatabaseContext.Reviews.Where(item => item.IsActive).Average(item => item.Rating).ToString("#.###")
                : "0.000";
        }

        public void InitializeSelectedBook(int bookId)
        {
            SelectedBookId = bookId;
            SelectedBookTotalAuthors = countAuthorsForBook(bookId);
            SelectedBookTotalCategories = countCategoriesForBook(bookId);
            SelectedBookTotalPublishers = countPublishersForBook(bookId);
            SelectedBookReviewRatingAverage = calculateBookAverageReviewRating(bookId);
            SelectedBookReviewRatingLargest = getLargestReviewRating(bookId);
            SelectedBookReviewRatingSmallest = getSmallestReviewRating(bookId);
        }

        private int countAuthorsForBook(int bookId)
        {
            return DatabaseContext.BookAuthors
                .Where(ba => ba.IsActive && ba.BookId == bookId && ba.Author.IsActive)
                .Count();
        }

        private int countCategoriesForBook(int bookId)
        {
            return DatabaseContext.BookCategories
                .Where(bc => bc.IsActive && bc.BookId == bookId && bc.Category.IsActive)
                .Count();
        }

        private int countPublishersForBook(int bookId)
        {
            return DatabaseContext.BookPublishers
                .Where(bp => bp.IsActive && bp.BookId == bookId && bp.Publisher.IsActive)
                .Count();
        }

        private string calculateBookAverageReviewRating(int bookId)
        {
            var reviews = DatabaseContext.Reviews.Where(r => r.BookId == bookId && r.IsActive);
            return reviews.Any() ? reviews.Average(r => r.Rating).ToString("#.###") : "0.000";
        }

        private string getLargestReviewRating(int bookId)
        {
            var reviews = DatabaseContext.Reviews.Where(r => r.BookId == bookId && r.IsActive);
            return reviews.Any() ? reviews.Max(r => r.Rating).ToString("#.###") : "0.000";
        }

        private string getSmallestReviewRating(int bookId)
        {
            var reviews = DatabaseContext.Reviews.Where(r => r.BookId == bookId && r.IsActive);
            return reviews.Any() ? reviews.Min(r => r.Rating).ToString("#.###") : "0.000";
        }
    }
}
