using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return DatabaseContext.Authors.First(item => item.Id == id);
        }

        public override List<AuthorDto> GetModels()
        {
            IQueryable<Author> authors = DatabaseContext.Authors.Include(item => item.Country).Where(item => item.IsActive);

            if (IsAliveCheck)
            {
                authors = authors.Where(item => item.IsAlive);
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
                LastName = item.LastName,
                Biography = item.Biography,
                CountryOrigin = item.Country.Title,
                FirstName = item.FirstName,
                Title = $"{item.FirstName.Trim()} {item.LastName.Trim()}",

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
            DatabaseContext.Authors.Update(model);
            DatabaseContext.SaveChanges();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return null;
        }
    }
}
