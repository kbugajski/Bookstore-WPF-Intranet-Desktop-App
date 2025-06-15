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
            get => Service.BookAuthors;
            set
            {
                if (Service.BookAuthors != value)
                {
                    Service.BookAuthors = value;
                    OnPropertyChanged(() => BookAuhtors);
                }
            }
        }


        public ObservableCollection<CheckablePublisherDto> BookPublishers
        {
            get => Service.BookPublishers;
            set
            {
                if (Service.BookPublishers != value)
                {
                    Service.BookPublishers = value;
                    OnPropertyChanged(() => BookPublishers);
                }
            }
        }
        public ObservableCollection<CheckableCategoryDto> BookCategories
        {
            get => Service.BookCategories;
            set
            {
                if (Service.BookCategories != value)
                {
                    Service.BookCategories = value;
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
            BookAuhtors = new ObservableCollection<CheckableAuthorDto>(Service.GetCheckableAuthorDtos(Model.Id));
            BookPublishers = new ObservableCollection<CheckablePublisherDto>(Service.GetCheckablePublisherDtos(Model.Id));
            BookCategories = new ObservableCollection<CheckableCategoryDto>(Service.GetCheckableCategoryDtos(Model.Id));
        }

       

    }
}
