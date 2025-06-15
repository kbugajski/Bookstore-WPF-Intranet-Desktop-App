using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Models.Contexts;
using BookstoreApp.Models.Dtos;

namespace BookstoreApp.Models.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DtoType">DTO do wyświetlenia na liście</typeparam>
    /// <typeparam name="ModelType">Model do dodania/edycji</typeparam>    
    public abstract class BaseService<DtoType, ModelType>
        where ModelType : new()
    {
        protected DatabaseContext DatabaseContext { get; set; }
        public string? SearchInput { get; set; }
        public string? SearchProperty { get; set; }
        public BaseService()
        {
            DatabaseContext = new DatabaseContext();
        }
        public abstract List<DtoType> GetModels();
        public abstract ModelType GetModel(int id);
        public abstract void AddModel(ModelType model);
        public abstract void UpdateModel(ModelType model);
        public abstract void DeleteModel(DtoType model);

        public abstract List<SearchComboBoxDto> GetSearchComboBoxDtos();
        public virtual ModelType CreateModel()
        {
            return new ModelType();
        }
    }
}
