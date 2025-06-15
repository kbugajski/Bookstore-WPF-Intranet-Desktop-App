using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Helpers;
using BookstoreApp.Models.Contexts;
using BookstoreApp.Models.Dtos;

namespace BookstoreApp.Models.Services
{
    public class BookService : BaseService<BookDto, Book>
    {
        
        
        public YesNoEnum HasIsbn { get; set; }

        public BookService()
        {
            HasIsbn = YesNoEnum.NoFilter;

        }
        public override void AddModel(Book model)
        {
            DatabaseContext.Books.Add(model);
            DatabaseContext.SaveChanges();
        }

        public override void DeleteModel(BookDto model)
        {
            Book book = DatabaseContext.Books.First(item => item.Id == model.Id);
            book.IsActive = false;
            book.DeleteDateTime = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override Book GetModel(int id)
        {
            return DatabaseContext.Books.First(item => item.Id == id);
        }

        public override List<BookDto> GetModels()
        {
            IQueryable<Book> books = DatabaseContext.Books.Where(item => item.IsActive);

            switch (HasIsbn)
            {
                case YesNoEnum.Yes:
                    books = books.Where(item => !string.IsNullOrEmpty(item.Isbn));
                    break;
                case YesNoEnum.No:
                    books = books.Where(item => string.IsNullOrEmpty(item.Isbn));
                    break;
            }

            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(Book.Title):
                        books = books.Where(item =>item.Title.Contains(SearchInput));
                        break;
                }
            }


            IQueryable<BookDto> booksDto = books.Select(item => new BookDto
            {
                Id = item.Id,
                Title = item.Title,
                Isbn = item.Isbn,

            });
            return booksDto.ToList();
        }

        public override Book CreateModel()
        {
            return new Book()
            {
                IsActive = true,
                CreationDateTime = DateTime.Now,
            };
        }

        public override void UpdateModel(Book model)
        {
            DatabaseContext.Books.Update(model);
            DatabaseContext.SaveChanges();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos() 
        { 
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropteryTitle = nameof(Book.Title),
                    DisplayName = "Title"
                },
                new SearchComboBoxDto()
                {
                    PropteryTitle = nameof(Book.Isbn),
                    DisplayName = "ISBN"
                }
            };
        }
    }
}
