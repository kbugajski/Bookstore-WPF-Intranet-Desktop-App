using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BookstoreApp.Helpers;
using BookstoreApp.Models.Contexts;
using BookstoreApp.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.Models.Services
{
    public class AuthorService : BaseService<AuthorDto, Author>
    {
        public bool IsAliveCheck { get; set; }
        public YesNoEnum HasBiography { get; set; }

        public AuthorService() { 
            HasBiography = YesNoEnum.NoFilter;
        }

        public override void AddModel(Author model)
        {
            DatabaseContext.Authors.Add(model);
            DatabaseContext.SaveChanges();
        }

        public override void DeleteModel(AuthorDto model)
        {
            Author author = DatabaseContext.Authors.First(item => item.Id == model.Id);
            author.IsActive = false;
            author.DeleteDateTime = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override Author GetModel(int id)
        {
            return DatabaseContext.Authors.Include(item => item.Country).First(item => item.Id == id);
        }

        public override List<AuthorDto> GetModels()
        {
            IQueryable<Author> authors = DatabaseContext.Authors.Include(item => item.Country).Where(item => item.IsActive);

            if (IsAliveCheck)
            {
                authors = authors.Where(item => item.IsAlive);
            }

            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(Author.FirstName):
                        authors= authors.Where(item =>
                         item.FirstName.ToLower().Contains(SearchInput.ToLower()) ||
                         item.LastName.ToLower().Contains(SearchInput.ToLower()));
                        break;
                    case nameof(Author.Country):
                        authors= authors.Where(item => item.Country.Title.Contains(SearchInput));
                        break;
                    case nameof(Author.Biography):
                        authors= authors.Where(item => item.Biography.Contains(SearchInput));
                        break;
                }
            }

            switch(OrderProperty)
            {
                case nameof(Author.LastName):
                    authors = OrderAscending? authors.OrderBy(item => item.LastName) : authors.OrderByDescending(item => item.LastName);
                    break;
                case nameof(Author.Country):
                    authors = OrderAscending ? authors.OrderBy(item => item.Country.Title) : authors.OrderByDescending(item => item.Country.Title);
                    break;
                case nameof(Author.Biography):
                    authors = OrderAscending ? authors.OrderBy(item => item.Biography) : authors.OrderByDescending(item => item.Biography);
                    break;
            }

            switch (HasBiography)
            {
                case YesNoEnum.Yes:
                    authors = authors.Where(item => !string.IsNullOrEmpty(item.Biography));
                    break;
                case YesNoEnum.No:
                    authors = authors.Where(item => string.IsNullOrEmpty(item.Biography));
                    break;
            }

            IQueryable<AuthorDto> authorsDto = authors.Select(item => new AuthorDto
            {
                Id = item.Id,
                LastName = item.LastName.Trim(),
                Biography = item.Biography,
                CountryOrigin = item.Country.Title,
                FirstName = item.FirstName.Trim(),
                IsAlive = item.IsAlive

            });
            return authorsDto.ToList();
        }

        public override Author CreateModel()
        {
            return new Author()
            {
                IsActive = true,
                CreationDateTime = DateTime.Now,
            };
        }

        public override void UpdateModel(Author model)
        {
            model.EditDateTime = DateTime.Now;
            DatabaseContext.Authors.Update(model);
            DatabaseContext.SaveChanges();
        }

        public override string ValidateProperty(string columnName, Author model)
        {
            if (columnName == nameof(Author.LastName))
            {
                if (string.IsNullOrWhiteSpace(model.LastName))
                    return "Last Name is required";

                if (!Regex.IsMatch(model.LastName, @"^[a-zA-Z\s-]+$"))
                    return "Last Name can only contain letters, spaces, and hyphens";
            }

            if (columnName == nameof(Author.FirstName))
            {
                if (string.IsNullOrWhiteSpace(model.FirstName))
                    return "First Name is required";

                if (!Regex.IsMatch(model.FirstName, @"^[a-zA-Z\s-]+$"))
                    return "First Name can only contain letters, spaces, and hyphens";
            }
            return string.Empty;
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto(){
                    PropertyTitle = nameof(Author.FirstName),
                    DisplayName = "Names"
                },
                new SearchComboBoxDto(){
                    PropertyTitle = nameof(Author.Country),
                    DisplayName = "Country"
                },
                new SearchComboBoxDto(){
                    PropertyTitle = nameof(Author.Biography),
                    DisplayName = "Biography"
                }
            };
        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto(){
                    PropertyTitle = nameof(Author.LastName),
                    DisplayName = "Last Name"
                },
                new SearchComboBoxDto(){
                    PropertyTitle = nameof(Author.Country),
                    DisplayName = "Country"
                },
                new SearchComboBoxDto(){
                    PropertyTitle = nameof(Author.Biography),
                    DisplayName = "Biography"
                }
            };
        }
    }
}
