using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BookstoreApp.Models;
using BookstoreApp.ViewModels;
using BookstoreApp.Models.Contexts;
using BookstoreApp.ViewModels;
using System.Windows.Input;
using BookstoreApp.Helpers;
using BookstoreApp.ViewModels.Single;
using BookstoreApp.Models.Dtos;
using BookstoreApp.Models.Services;
using CommunityToolkit.Mvvm.Messaging;

namespace BookstoreApp.ViewModels.Many;

public class BooksViewModel :BaseManyViewModel<BookService, BookDto, Book>
{
    public YesNoEnum HasIsbn
    {
        get => Service.HasIsbn;
        set
        {
            if (Service.HasIsbn != value)
            {
                Service.HasIsbn = value;
                OnPropertyChanged(() => HasIsbn);
            }
        }
    }
    public BooksViewModel() : base("Book")
    {
    }

    protected override void ClearFilter()
    {
        throw new NotImplementedException();
    }

    protected override void CreateNew()
    {
        WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
        {
            Sender = this,
            ViewModelToBeOpened = new AddBookViewModel()
        });
    }
}

