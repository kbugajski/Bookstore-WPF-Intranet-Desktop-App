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

namespace BookstoreApp.ViewModels.Many
{
    public class CategoriesViewModel : WorkspaceViewModel
    {
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        protected DatabaseContext Database { get; private set; }
        private ObservableCollection<Category> _Models;
        public ObservableCollection<Category> Models
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

        private Category? _SelectedModel;
        public Category? SelectedModel
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
        public CategoriesViewModel() : base("Categories")
        {

            RefreshCommand = new BaseCommand(() => Initialize());
            DeleteCommand = new BaseCommand(() => Delete());
            Initialize();
        }

        private void Initialize()
        {
            Database = new DatabaseContext();
            List<Category> categories = Database.Categories.Where(x => x.IsActive).ToList();
            Models = new ObservableCollection<Category>(categories);
        }

        public void Delete()
        {
            if (_Models != null)
            {
                if (SelectedModel != null)
                {
                    Category? category = Database.Categories.Find(SelectedModel.Id);
                    if (category != null)
                    {
                        category.IsActive = false;
                        category.DeleteDateTime = DateTime.Now;
                        Database.SaveChanges();
                        Models.Remove(category);
                    }
                }

            }
        }
    }
}
