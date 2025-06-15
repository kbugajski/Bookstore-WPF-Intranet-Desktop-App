using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Models;
using BookstoreApp.Models.Dtos;
using BookstoreApp.Models.Services;

namespace BookstoreApp.ViewModels.Single
{
    public class AddCategoryViewModel : BaseCreateViewModel<CategoryService, CategoryDto, Category>
    {
        public AddCategoryViewModel() : base("Add Category")
        {
            TopLabel = "Add new book category";
        }

        public AddCategoryViewModel(Category model) : base("Edit Category", model)
        {
            TopLabel = "Edit Category";
        }



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
    }
}
