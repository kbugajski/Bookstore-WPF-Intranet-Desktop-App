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

namespace BookstoreApp.ViewModels.Single
{
    public class BaseCreateViewModel<ServiceType, DtoType, ModelType>
        : BaseServiceViewModel<ServiceType, DtoType, ModelType>
        where ServiceType : BaseService<DtoType, ModelType>, new()
        where DtoType : class
        where ModelType : class, new()
    {
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
       


        public BaseCreateViewModel(string displayName) : base(displayName)
        {
            DbContext = new DatabaseContext();
            _Model = Service.CreateModel();
            SaveCommand = new BaseCommand(() => Save());
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
            CustomerStatusesComboBoxDto = new ObservableCollection<ComboBoxDto>(DbContext.CustomerCategories.Where(item => item.IsActive)
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
        public virtual void Save()
        {
            Service.AddModel(Model);
            OnRequestClose();
        }
        public void Cancel()
        {
            OnRequestClose();
        }
    }
}
