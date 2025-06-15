using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Helpers;
using BookstoreApp.Models.Services;
using System.Windows.Input;
using BookstoreApp.Models.Dtos;
using BookstoreApp.Models.Contexts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using BookstoreApp.ViewModels.Many;

namespace BookstoreApp.ViewModels.Single
{
    public class BaseCreateViewModel<ServiceType, DtoType, ModelType>
        : BaseServiceViewModel<ServiceType, DtoType, ModelType>, IDataErrorInfo
        where ServiceType : BaseService<DtoType, ModelType>, new()
        where DtoType : class
        where ModelType : class, new()
    {

        public string TopLabel { get; set; }
        
        private DatabaseContext DbContext { get; set; }
        private ModelType _Model;
        public ModelType Model
        {
            get => _Model;
            set
            {
                if (_Model != value)
                {
                    _Model = value;
                    OnPropertyChanged(() => Model);
                }
            }
        }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ObservableCollection<ComboBoxDto> CountriesComboBoxDto { get; set; }
        public ObservableCollection<ComboBoxDto> PaymentMethodsComboBoxDto { get; set; }
        public ObservableCollection<ComboBoxDto> CustomerStatusesComboBoxDto { get; set; }
        public ObservableCollection<ComboBoxDto> CustomerCategoriesComboBoxDto { get; set; }
        public ObservableCollection<ComboBoxDto> BooksComboBoxDto { get; set; }
        public ObservableCollection<ComboBoxDto> CustomersComboBoxDto { get; set; }

        public string Error => string.Empty;

        public string this[string columnName]
        {
            get 
            {
                return Service.ValidateProperty(columnName, Model);
            }
            set { }
        }
        public BaseCreateViewModel(string displayName, ModelType model) :base(displayName)
        {
            DbContext = new DatabaseContext();
            Model = model;
            SaveCommand = new BaseCommand(() => SaveModel());
            CancelCommand = new BaseCommand(() => Cancel());
            CountriesComboBoxDto = new ObservableCollection<ComboBoxDto>(DbContext.Countries.Where(item => item.IsActive)
                                                                           .Select(item => new ComboBoxDto()
                                                                           {
                                                                               Id = item.Id,
                                                                               Title = item.Title,
                                                                           }).ToList());
            PaymentMethodsComboBoxDto = new ObservableCollection<ComboBoxDto>(DbContext.PaymentMethods.Where(item => item.IsActive)
                                                                           .Select(item => new ComboBoxDto()
                                                                           {
                                                                               Id = item.Id,
                                                                               Title = item.Title,
                                                                           }).ToList());
            CustomerStatusesComboBoxDto = new ObservableCollection<ComboBoxDto>(DbContext.CustomerStatuses.Where(item => item.IsActive)
                                                                           .Select(item => new ComboBoxDto()
                                                                           {
                                                                               Id = item.Id,
                                                                               Title = item.Title,
                                                                           }).ToList());
            CustomerCategoriesComboBoxDto = new ObservableCollection<ComboBoxDto>(DbContext.CustomerCategories.Where(item => item.IsActive)
                                                                           .Select(item => new ComboBoxDto()
                                                                           {
                                                                               Id = item.Id,
                                                                               Title = item.Title,
                                                                           }).ToList());
            BooksComboBoxDto = new ObservableCollection<ComboBoxDto>(DbContext.Books.Where(item => item.IsActive)
                                                                           .Select(item => new ComboBoxDto()
                                                                           {
                                                                               Id = item.Id,
                                                                               Title = item.Title,
                                                                           }).ToList());
            CustomersComboBoxDto = new ObservableCollection<ComboBoxDto>(DbContext.Customers.Where(item => item.IsActive)
                                                                           .Select(item => new ComboBoxDto()
                                                                           {
                                                                               Id = item.Id,
                                                                               Title = $"{item.FirstName.Trim()} {item.LastName.Trim()}"
                                                                           }).ToList());
        }
        
        public BaseCreateViewModel(string displayName) : base(displayName)
        {
            DbContext = new DatabaseContext();
            Model = Service.CreateModel();
            SaveCommand = new BaseCommand(() => CreateModel());
            CancelCommand = new BaseCommand(() => Cancel());
            CountriesComboBoxDto = new ObservableCollection<ComboBoxDto>(DbContext.Countries.Where(item => item.IsActive)
                                                                           .Select(item => new ComboBoxDto()
                                                                           {
                                                                               Id = item.Id,
                                                                               Title = item.Title,
                                                                           }).ToList());
            PaymentMethodsComboBoxDto = new ObservableCollection<ComboBoxDto>(DbContext.PaymentMethods.Where(item => item.IsActive)
                                                                           .Select(item => new ComboBoxDto()
                                                                           {
                                                                               Id = item.Id,
                                                                               Title = item.Title,
                                                                           }).ToList());
            CustomerStatusesComboBoxDto = new ObservableCollection<ComboBoxDto>(DbContext.CustomerStatuses.Where(item => item.IsActive)
                                                                           .Select(item => new ComboBoxDto()
                                                                           {
                                                                               Id = item.Id,
                                                                               Title = item.Title,
                                                                           }).ToList());
            CustomerCategoriesComboBoxDto = new ObservableCollection<ComboBoxDto>(DbContext.CustomerCategories.Where(item => item.IsActive)
                                                                           .Select(item => new ComboBoxDto()
                                                                           {
                                                                               Id = item.Id,
                                                                               Title = item.Title,
                                                                           }).ToList());
            BooksComboBoxDto = new ObservableCollection<ComboBoxDto>(DbContext.Books.Where(item => item.IsActive)
                                                                           .Select(item => new ComboBoxDto()
                                                                           {
                                                                               Id = item.Id,
                                                                               Title = item.Title,
                                                                           }).ToList());
            CustomersComboBoxDto = new ObservableCollection<ComboBoxDto>(DbContext.Customers.Where(item => item.IsActive)
                                                                           .Select(item => new ComboBoxDto()
                                                                           {
                                                                               Id = item.Id,
                                                                               Title = $"{item.FirstName.Trim()} {item.LastName.Trim()}"
                                                                           }).ToList());
        }
        public virtual void CreateModel()
        {
            try
            {
                if (Service.isValid(Model))
                { 
                    Service.AddModel(Model);
                    OnRequestClose();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error on save. Check if all necessery fields are filled correctly", "Error");
            }
            
        }
        public virtual void SaveModel() 
        {
            try
            {
                if (Service.isValid(Model))
                {
                    Service.UpdateModel(Model);
                    OnRequestClose();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on save. Check if all necessery fields are filled correctly", "Error");
            }
        }

        public void Cancel()
        {
            OnRequestClose();
        }

        

       
    }
}
