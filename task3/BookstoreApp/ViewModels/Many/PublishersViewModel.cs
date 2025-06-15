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
using System.Security.Policy;
using Publisher = BookstoreApp.Models.Publisher;

namespace BookstoreApp.ViewModels.Many
{
    public class PublishersViewModel : WorkspaceViewModel
    {
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        protected DatabaseContext Database { get; private set; }
        private ObservableCollection<Publisher> _Models;
        public ObservableCollection<Publisher> Models
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

        private Publisher? _SelectedModel;
        public Publisher? SelectedModel
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
        public PublishersViewModel() : base("Publishers")
        {

            RefreshCommand = new BaseCommand(() => Initialize());
            DeleteCommand = new BaseCommand(() => Delete());
            Initialize();
        }

        private void Initialize()
        {
            Database = new DatabaseContext();
            List<Publisher> publisher = Database.Publishers.Where(x => x.IsActive).ToList();
            Models = new ObservableCollection<Publisher>(publisher);
        }

        public void Delete()
        {
            if (_Models != null)
            {
                if (SelectedModel != null)
                {
                    Publisher? publisher = Database.Publishers.Find(SelectedModel.Id);
                    if (publisher != null)
                    {
                        publisher.IsActive = false;
                        publisher.DeleteDateTime = DateTime.Now;
                        Database.SaveChanges();
                        Models.Remove(publisher);
                    }
                }

            }
        }
    }
}
