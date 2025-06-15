using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BookstoreApp.Helpers;
using BookstoreApp.Models;
using BookstoreApp.Models.Contexts;
using BookstoreApp.Models.Dtos;
using BookstoreApp.Models.Services;
using Microsoft.IdentityModel.Tokens;

namespace BookstoreApp.ViewModels.Single
{
    public class AddBookViewModel : BaseCreateViewModel<BookService, BookDto, Book>
    {
        public string Title
        {
            get => Model.Title;
            set
            {
                if (Model.Title != value)
                {
                    Model.Title = value;
                    OnPropertyChanged(() => Title);
                }

            }
        }

        public string Isbn
        {
            get => Model.Isbn;
            set
            {
                if (Model.Isbn != value)
                    Model.Isbn = value;
                OnPropertyChanged(() => Isbn);
            }
        }
        
        public ObservableCollection<CheckableAuthorDto> BookAuhtors
        {
            get => Service.BookAuthorDtos;
            set
            {
                if (Service.BookAuthorDtos != value)
                {
                    Service.BookAuthorDtos = value;
                    OnPropertyChanged(() => BookAuhtors);
                }
            }
        }


        public ObservableCollection<CheckablePublisherDto> BookPublishers
        {
            get => Service.BookPublisherDtos;
            set
            {
                if (Service.BookPublisherDtos != value)
                {
                    Service.BookPublisherDtos = value;
                    OnPropertyChanged(() => BookPublishers);
                }
            }
        }
        public ObservableCollection<CheckableCategoryDto> BookCategories
        {
            get => Service.BookCategoryDtos;
            set
            {
                if (Service.BookCategoryDtos != value)
                {
                    Service.BookCategoryDtos = value;
                    OnPropertyChanged(() => BookCategories);
                }
            }
        }

        public AddBookViewModel() : base("Add book")
        {
            TopLabel = "Add new Book";
            Refresh();
        }

        public AddBookViewModel(Book model) : base("Edit book", model)
        {
            TopLabel = "Edit Book";
            Refresh();
        }

        protected void Refresh()
        {
            BookAuhtors = new ObservableCollection<CheckableAuthorDto>(Service.GetCheckableAuthorDtos(Model?.Id ?? 0));
            BookPublishers = new ObservableCollection<CheckablePublisherDto>(Service.GetCheckablePublisherDtos(Model?.Id ?? 0));
            BookCategories = new ObservableCollection<CheckableCategoryDto>(Service.GetCheckableCategoryDtos(Model?.Id ?? 0));
        }

       

    }
}
