using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookstoreApp.Helpers;
using BookstoreApp.Models;
using BookstoreApp.Models.Contexts;

namespace BookstoreApp.ViewModels.Many
{
    public class ReviewsViewModel : WorkspaceViewModel
    {
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        protected DatabaseContext Database { get; private set; }
        private ObservableCollection<Review> _Models;
        public ObservableCollection<Review> Models
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

        private Review? _SelectedModel;
        public Review? SelectedModel
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
        public ReviewsViewModel() : base("Publishers")
        {

            RefreshCommand = new BaseCommand(() => Initialize());
            DeleteCommand = new BaseCommand(() => Delete());
            Initialize();
        }

        private void Initialize()
        {
            Database = new DatabaseContext();
            List<Review> reviews = Database.Reviews.Where(x => x.IsActive).ToList();
            Models = new ObservableCollection<Review>(reviews);
        }

        public void Delete()
        {
            if (_Models != null)
            {
                if (SelectedModel != null)
                {
                    Review? review = Database.Reviews.Find(SelectedModel.Id);
                    if (review != null)
                    {
                        review.IsActive = false;
                        review.DeleteDateTime = DateTime.Now;
                        Database.SaveChanges();
                        Models.Remove(review);
                    }
                }

            }
        }
    }
}

