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
using BookstoreApp.Models.Dtos;
using BookstoreApp.Models.Services;
using BookstoreApp.ViewModels.Single;
using CommunityToolkit.Mvvm.Messaging;

namespace BookstoreApp.ViewModels.Many
{
    public class CategoriesViewModel : BaseManyViewModel<CategoryService, CategoryDto, Category>
    {

        public CategoriesViewModel() : base("Categories")
        {

        }

        protected override void ClearFilter()
        {
            SearchInput = string.Empty;
            Refresh();
        }

        protected override void CreateNew()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new AddCategoryViewModel()
            });
        }

        protected override void Edit()
        {
            if (SelectedModel != null)
            {
                Category modelToEdit = Service.GetModel(SelectedModel.Id);
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
                {
                    Sender = this,
                    ViewModelToBeOpened = new AddCategoryViewModel(modelToEdit)
                });
            }
        }
    }
}
