using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Helpers;
using BookstoreApp.Models.Contexts;
using BookstoreApp.Models;
using System.Windows.Input;
using BookstoreApp.ViewModels;

namespace BookstoreApp.ViewModels.Many
{
    public class AuthorsViewModel : WorkspaceViewModel
    {
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        protected DatabaseContext Database { get; private set; }
        private ObservableCollection<Author> _Models;
        public ObservableCollection<Author> Models
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

        private Author? _SelectedModel;
        public Author? SelectedModel
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
        public AuthorsViewModel() : base("Authors")
        {

            RefreshCommand = new BaseCommand(() => Initialize());
            DeleteCommand = new BaseCommand(() => Delete());
            Initialize();
        }

        private void Initialize()
        {
            Database = new DatabaseContext();
            List<Author> Authors = Database.Authors.Where(x => x.IsActive).ToList();
            Models = new ObservableCollection<Author>(Authors);
        }

        public void Delete()
        {
            if (_Models != null)
            {
                if(SelectedModel != null)
                {
                    Author? Author = Database.Authors.Find(SelectedModel.Id);
                    if (Author != null)
                    {
                        Author.IsActive = false;
                        Author.DeleteDateTime = DateTime.Now;
                        Database.SaveChanges();
                        Models.Remove(Author);
                    }
                }
                
            }
        }
    }
}

