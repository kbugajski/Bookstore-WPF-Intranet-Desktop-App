using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Helpers;
using BookstoreApp.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.Models.Services
{
    public class ReviewService : BaseService<ReviewDto, Review>
    {
        public double Rating { get; set; }
        public YesNoEnum HasReviewText { get; set; }
        public LargerSmallerEnum SelectedLargerSmallerOption { get; set; }

        public ReviewService()
        {
            SelectedLargerSmallerOption = LargerSmallerEnum.NoFilter;
            HasReviewText = YesNoEnum.NoFilter;
            Rating = 1;
        }
        public override void AddModel(Review model)
        {
            DatabaseContext.Reviews.Add(model);
            DatabaseContext.SaveChanges();
        }


        public override void DeleteModel(ReviewDto model) 
        {
            Review review = DatabaseContext.Reviews.First(item => item.Id == model.Id);
            review.IsActive = false;
            review.DeleteDateTime = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override Review GetModel(int id)
        {
            return DatabaseContext.Reviews.Include(item => item.Customer).Include(item => item.Book).First(item => item.Id == id);
        }

        public override List<ReviewDto> GetModels()
        {
            IQueryable<Review> reviews = DatabaseContext.Reviews.Include(item => item.Book).Include(item => item.Customer)
                                                                    .Where(item => item.IsActive);

            switch (HasReviewText)
            {
                case YesNoEnum.Yes:
                    reviews = reviews.Where(item => !string.IsNullOrEmpty(item.ReviewText));
                    break;
                case YesNoEnum.No:
                    reviews = reviews.Where(item => string.IsNullOrEmpty(item.ReviewText));
                    break;
            }

            switch (OrderProperty)
            {
                case (nameof(Review.Customer)):
                    reviews = OrderAscending ? reviews.OrderBy(item => item.Customer.LastName) : reviews.OrderByDescending(item => item.Customer.LastName);
                    break;
                case nameof(Review.Book):
                    reviews = OrderAscending ? reviews.OrderBy(item => item.Book.Title) : reviews.OrderByDescending(item => item.Book.Title);
                    break;
                case nameof(Review.ReviewText):
                    reviews = OrderAscending ? reviews.OrderBy(item => item.ReviewText) : reviews.OrderByDescending(item => item.ReviewText);
                    break;
                case nameof(Review.ReviewDate):
                    reviews = OrderAscending ? reviews.OrderBy(item => item.ReviewDate) : reviews.OrderByDescending(item => item.ReviewDate);
                    break;

            }

            switch (SelectedLargerSmallerOption)
            {
                case LargerSmallerEnum.Equal:
                    reviews = reviews.Where(item => item.Rating == Rating);
                    break;
                case LargerSmallerEnum.Larger:
                    reviews = reviews.Where(item => item.Rating >= Rating);
                    break;
                case LargerSmallerEnum.Smaller:
                    reviews = reviews.Where(item => item.Rating <= Rating);
                    break;
            }


            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(Review.Customer):
                        reviews = reviews.Where(item =>
                         item.Customer.FirstName.ToLower().Contains(SearchInput.ToLower()) ||
                         item.Customer.LastName.ToLower().Contains(SearchInput.ToLower()));
                        break;
                    case nameof(Review.Book):
                        reviews = reviews.Where(item => item.Book.Title.Contains(SearchInput));
                        break;
                    case nameof(Review.ReviewText):
                        reviews = reviews.Where(item => item.ReviewText.Contains(SearchInput));
                        break;
                }
            }


            IQueryable<ReviewDto> reviewsDto = reviews.Select(item => new ReviewDto
            {
                BookName = item.Book.Title,
                CustomerName = $"{item.Customer.FirstName.Trim()} {item.Customer.LastName.Trim()}",
                Id = item.Id,
                Rating = item.Rating,
                ReviewDate = item.ReviewDate,
                ReviewText = item.ReviewText,

            });
            return reviewsDto.ToList();
        }

        public override Review CreateModel()
        {
            return new Review()
            {
                IsActive = true,
                CreationDateTime = DateTime.Now,
            };
        }

        public override void UpdateModel(Review model)
        {
            model.Book = DatabaseContext.Books.FirstOrDefault(book => book.Id == model.BookId);
            model.Customer = DatabaseContext.Customers.FirstOrDefault(item => item.Id == model.CustomerId);
            model.EditDateTime = DateTime.Now;
            DatabaseContext.Reviews.Update(model);
            DatabaseContext.SaveChanges();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                //new SearchComboBoxDto()
                //{
                //    PropertyTitle = nameof(Customer.FirstName),
                //    DisplayName = "By First Name"
                //},
                //new SearchComboBoxDto()
                //{
                //    PropertyTitle = nameof(Customer.LastName),
                //    DisplayName = "By Last Name"
                //},
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Review.Customer),
                    DisplayName = "Reviewer"
                },

                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Review.Book),
                    DisplayName = "Book"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Review.ReviewText),
                    DisplayName = "Text"
                },
            };
        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                //new SearchComboBoxDto()
                //{
                //    PropertyTitle = nameof(Customer.FirstName),
                //    DisplayName = "By First Name"
                //},
                //new SearchComboBoxDto()
                //{
                //    PropertyTitle = nameof(Customer.LastName),
                //    DisplayName = "By Last Name"
                //},
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Review.Customer),
                    DisplayName = "Reviewer"
                },

                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Review.Book),
                    DisplayName = "Book"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Review.ReviewText),
                    DisplayName = "Text"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle= nameof(Review.ReviewDate),
                    DisplayName = "Review Date",
                }
            };
        }
        public override string ValidateProperty(string columnName, Review model)
        { 
            return string.Empty;
        }
    }
}

