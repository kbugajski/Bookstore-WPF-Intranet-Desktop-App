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
            return DatabaseContext.Reviews.First(item => item.Id == id);
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

            reviews = reviews.Where(item => item.Rating >= Rating);

           


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
            DatabaseContext.Reviews.Update(model);
            DatabaseContext.SaveChanges();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            throw new NotImplementedException();
        }
    }
}

