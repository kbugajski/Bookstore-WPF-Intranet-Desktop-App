using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Helpers;
using BookstoreApp.Models.Contexts;
using BookstoreApp.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.Models.Services
{
    public class BookService : BaseService<BookDto, Book>
    {
        public ObservableCollection<CheckableAuthorDto> BookAuthors { get; set; }
        public ObservableCollection<CheckablePublisherDto> BookPublishers { get; set; }
        public ObservableCollection<CheckableCategoryDto> BookCategories { get; set; }

        public YesNoEnum HasIsbn { get; set; }

        public BookService()
        {
            HasIsbn = YesNoEnum.NoFilter;

        }
        public override void AddModel(Book model)
        {
            DatabaseContext.Books.Add(model);
            DatabaseContext.SaveChanges();

            
            if (BookAuthors != null)
            {
                foreach (var author in BookAuthors.Where(a => a.IsChecked))
                {
                    var bookAuthor = DatabaseContext.BookAuthors
                        .FirstOrDefault(ba => ba.BookId == model.Id && ba.AuthorId == author.Id);

                    if (bookAuthor != null)
                    {
                        bookAuthor.IsActive = true;
                    }
                    else
                    {
                        DatabaseContext.BookAuthors.Add(new BookAuthor
                        {
                            BookId = model.Id,
                            AuthorId = author.Id,
                            IsActive = true
                        });
                    }
                }
            }

            if (BookPublishers != null)
            {
                foreach (var publisher in BookPublishers.Where(p => p.IsChecked))
                {
                    var bookPublisher = DatabaseContext.BookPublishers
                        .FirstOrDefault(bp => bp.BookId == model.Id && bp.PublisherId == publisher.Id);

                    if (bookPublisher != null)
                    {
                        bookPublisher.IsActive = true;
                    }
                    else
                    {
                        DatabaseContext.BookPublishers.Add(new BookPublisher
                        {
                            BookId = model.Id,
                            PublisherId = publisher.Id,
                            IsActive = true
                        });
                    }
                }
            }


            if (BookCategories != null)
            {
                foreach (var category in BookCategories.Where(c => c.IsChecked))
                {
                    var bookCategory = DatabaseContext.BookCategories
                        .FirstOrDefault(bc => bc.BookId == model.Id && bc.CategoryId == category.Id);

                    if (bookCategory != null)
                    {
                        bookCategory.IsActive = true;
                    }
                    else
                    {
                        DatabaseContext.BookCategories.Add(new BookCategory
                        {
                            BookId = model.Id,
                            CategoryId = category.Id,
                            IsActive = true
                        });
                    }
                }
            }

            DatabaseContext.SaveChanges();
        }

        public override void UpdateModel(Book model)
        {
            model.EditDateTime = DateTime.Now;
            DatabaseContext.Books.Update(model);
            DatabaseContext.SaveChanges();

            // Aktualizacja autorów książki
            if (BookAuthors != null)
            {
                foreach (var author in BookAuthors)
                {
                    var bookAuthor = DatabaseContext.BookAuthors
                        .FirstOrDefault(ba => ba.BookId == model.Id && ba.AuthorId == author.Id);

                    if (bookAuthor != null)
                    {
                        bookAuthor.IsActive = author.IsChecked;
                    }
                    else if (author.IsChecked)
                    {
                        DatabaseContext.BookAuthors.Add(new BookAuthor
                        {
                            BookId = model.Id,
                            AuthorId = author.Id,
                            IsActive = true
                        });
                    }
                }
            }

            // Aktualizacja wydawców książki
            if (BookPublishers != null)
            {
                foreach (var publisher in BookPublishers)
                {
                    var bookPublisher = DatabaseContext.BookPublishers
                        .FirstOrDefault(bp => bp.BookId == model.Id && bp.PublisherId == publisher.Id);

                    if (bookPublisher != null)
                    {
                        bookPublisher.IsActive = publisher.IsChecked;
                    }
                    else if (publisher.IsChecked)
                    {
                        DatabaseContext.BookPublishers.Add(new BookPublisher
                        {
                            BookId = model.Id,
                            PublisherId = publisher.Id,
                            IsActive = true
                        });
                    }
                }
            }

            // Aktualizacja kategorii książki
            if (BookCategories != null)
            {
                foreach (var category in BookCategories)
                {
                    var bookCategory = DatabaseContext.BookCategories
                        .FirstOrDefault(bc => bc.BookId == model.Id && bc.CategoryId == category.Id);

                    if (bookCategory != null)
                    {
                        bookCategory.IsActive = category.IsChecked;
                    }
                    else if (category.IsChecked)
                    {
                        DatabaseContext.BookCategories.Add(new BookCategory
                        {
                            BookId = model.Id,
                            CategoryId = category.Id,
                            IsActive = true
                        });
                    }
                }
            }

            DatabaseContext.SaveChanges();
        }

        public List<CheckableAuthorDto> GetCheckableAuthorDtos(int bookId)
        {
            return DatabaseContext.Authors
                .Where(author => author.IsActive)
                .Select(author => new CheckableAuthorDto
                {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    IsChecked = DatabaseContext.BookAuthors
                        .Any(ba => ba.BookId == bookId && ba.AuthorId == author.Id && ba.IsActive)
                })
                .ToList();
        }

        public List<CheckablePublisherDto> GetCheckablePublisherDtos(int bookId)
        {
            return DatabaseContext.Publishers
                .Where(publisher => publisher.IsActive)
                .Select(publisher => new CheckablePublisherDto
                {
                    Id = publisher.Id,
                    Name = publisher.Name,
                    IsChecked = DatabaseContext.BookPublishers
                        .Any(bp => bp.BookId == bookId && bp.PublisherId == publisher.Id && bp.IsActive)
                })
                .ToList();
        }

        public List<CheckableCategoryDto> GetCheckableCategoryDtos(int bookId)
        {
            return DatabaseContext.Categories
                .Where(category => category.IsActive)
                .Select(category => new CheckableCategoryDto
                {
                    Id = category.Id,
                    Title = category.Title,
                    IsChecked = DatabaseContext.BookCategories
                        .Any(bc => bc.BookId == bookId && bc.CategoryId == category.Id && bc.IsActive)
                })
                .ToList();
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
                        books = books.Where(item =>item.Title.ToLower().Contains(SearchInput.ToLower()));
                        break;
                    case nameof(Book.Isbn):
                        books = books.Where(item => item.Isbn.ToLower().Contains(SearchInput.ToLower()));
                        break;
                }
            }

            switch (OrderProperty)
            {
                case nameof(Book.Title):
                    books = OrderAscending ? books.OrderBy(item => item.Title) : books.OrderByDescending(item => item.Title);
                    break;
                case nameof(Book.Isbn):
                    books = OrderAscending ? books.OrderBy(item => item.Isbn) : books.OrderByDescending(item => item.Isbn);
                    break;
            }

           

            IQueryable<BookDto> booksDto = books.Select(item => new BookDto
            {
                Id = item.Id,
                Title = item.Title,
                BookCategoriesTitles = string.Join(", ", item.BookCategories
                          .Where(bc => bc.Category.IsActive && bc.IsActive)
                          .Select(bc => bc.Category.Title)),
                BookPublishersTitles = string.Join(", ", item.BookPublishers
                          .Where(bp => bp.Publisher.IsActive && bp.IsActive)
                          .Select(bp => bp.Publisher.Name)),
                BookAuthorsTitles = string.Join(", ", item.BookAuthors
                          .Where(ba => ba.Author.IsActive && ba.IsActive)
                          .Select(ba => ba.Author.FirstName.Trim() + " " + ba.Author.LastName.Trim())),
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
    

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos() 
        { 
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Book.Title),
                    DisplayName = "Title"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Book.Isbn),
                    DisplayName = "ISBN"
                },
            };
        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Book.Title),
                    DisplayName = "Title"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Book.Isbn),
                    DisplayName = "ISBN"
                }
            };
        }

        public override string ValidateProperty(string columnName, Book model)
        {
            if(columnName == nameof(Book.Title))
            {
                if (string.IsNullOrWhiteSpace(model.Title))
                    return "Title is required";
            }
            if(columnName == nameof(Book.Isbn))
            {
                if (string.IsNullOrWhiteSpace(model.Isbn))
                {
                    return "ISBN code is required";
                }
                if (!int.TryParse(model.Isbn, out int a))
                {
                    return "ISBN code must be numeric";
                }
            }
            return string.Empty;
        }
    }
}
