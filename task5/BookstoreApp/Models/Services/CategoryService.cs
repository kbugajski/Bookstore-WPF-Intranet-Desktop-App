using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Models.Contexts;
using BookstoreApp.Models.Dtos;

namespace BookstoreApp.Models.Services
{
    public class CategoryService : BaseService<CategoryDto, Category>
    {
        public override void AddModel(Category model)
        {
            DatabaseContext.Categories.Add(model);
            DatabaseContext.SaveChanges();
        }

        public override void DeleteModel(CategoryDto model)
        {
            Category category = DatabaseContext.Categories.First(item => item.Id == model.Id);
            category.IsActive = false;
            category.DeleteDateTime = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override Category GetModel(int id)
        {
            return DatabaseContext.Categories.First(item => item.Id == id);
        }

        public override List<CategoryDto> GetModels()
        {
            IQueryable<Category> categories = DatabaseContext.Categories
                                                            .Where(item => item.IsActive);
            if (!string.IsNullOrEmpty(SearchInput))
            {
                categories = categories.Where(item => item.Title.ToLower().Contains(SearchInput.ToLower()));
            }
            IQueryable<CategoryDto> categoryDto = categories.Select(item => new CategoryDto()
            {
                Id = item.Id,
                Title = item.Title,
                CreationDateTime = item.CreationDateTime,
                DeleteDateTime = item.DeleteDateTime,
                EditDateTime = item.EditDateTime

            });
            return categoryDto.ToList();
        }

        public override Category CreateModel()
        {
            return new Category()
            {
                IsActive = true,
                CreationDateTime = DateTime.Now
            };
        }

        public override void UpdateModel(Category model)
        {
            model.EditDateTime = DateTime.Now;
            DatabaseContext.Categories.Update(model);
            DatabaseContext.SaveChanges();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>();
        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>();
        }

        public override string ValidateProperty(string columnName, Category model)
        {
            if (columnName == nameof(Category.Title))
            {
                if (string.IsNullOrEmpty(model.Title))
                    return "Title is required";
            }

            return string.Empty;
        }
    }
}
