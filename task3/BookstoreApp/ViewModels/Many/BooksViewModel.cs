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

namespace BookstoreApp.ViewModels.Many;

public class BooksViewModel : WorkspaceViewModel
{
    public ICommand RefreshCommand { get; set; }
    public ICommand DeleteCommand { get; set; }
    protected DatabaseContext Database { get; private set; }
    private ObservableCollection<Book> _Models;
    public ObservableCollection<Book> Models
    {
        get => _Models;
        set
        {
            if (value != _Models)
            {
                _Models = value;
                OnPropertyChanged(() => Models);
            }
        }
    }

    private Book? _SelectedModel;
    public Book? SelectedModel
    {
        get => _SelectedModel;
        set
        {
            if (value != _SelectedModel)
            {
                _SelectedModel = value;
                OnPropertyChanged(() => SelectedModel);
            }
        }
    }
    public BooksViewModel() : base("Books")
    {

        RefreshCommand = new BaseCommand(() => Initialize());
        DeleteCommand = new BaseCommand(() => Delete());
        Initialize();
    }

    private void Initialize()
    {
        Database = new DatabaseContext();
        List <Book> books = Database.Books.Where(x => x.IsActive).ToList();
        Models = new ObservableCollection<Book>(books);
    }

    public void Delete()
    {
        if(_Models != null)
        {
            if (SelectedModel != null)
            {
                Book? book = Database.Books.Find(SelectedModel.Id);
                if (book != null)
                {
                    book.IsActive = false;
                    book.DeleteDateTime = DateTime.Now;
                    Database.SaveChanges();
                    Models.Remove(book);
                }
            }
          
        }
    }
}

