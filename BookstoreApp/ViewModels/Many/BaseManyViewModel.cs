using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Helpers;
using BookstoreApp.Models.Services;
using System.Windows.Input;
using System.Windows.Data;
using BookstoreApp.Models.Dtos;
using BookstoreApp.Models;
using BookstoreApp.ViewModels.Single;

namespace BookstoreApp.ViewModels.Many
{
    abstract public class BaseManyViewModel<ServiceType, DtoType, ModelType>
        : BaseServiceViewModel<ServiceType, DtoType, ModelType>
        where ServiceType : BaseService<DtoType, ModelType>, new()
        where DtoType : class
        where ModelType : new()
        {
        public List<YesNoComboBoxItemDto> YesNoOptions { get; set; }
        public List<LargerSmallerComboBoxItemDto> LargerSmallerOptions { get; set; }
        public List<SearchComboBoxDto> SearchComboBoxDto { get; set; }
        public List<SearchComboBoxDto> OrderByComboBoxDto { get; set; }


        private ObservableCollection<DtoType> _Models;
        public ObservableCollection<DtoType> Models
        {
            get => _Models;
            set
            {
                if (_Models != value)
                {
                    _Models = value;
                    OnPropertyChanged(() => Models);
                }
            }
        }
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand ClearFiltersCommand { get; set; }
        public ICommand CreateNewCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public string? SearchInput
        {
            get => Service.SearchInput;
            set
            {
                if (Service.SearchInput != value)
                {
                    Service.SearchInput = value;
                    OnPropertyChanged(() => SearchInput);
                    //Refresh();
                }
            }
        }

        public string? SearchProperty
        {
            get => Service.SearchProperty;
            set
            {
                if (Service.SearchProperty != value)
                {
                    Service.SearchProperty = value;
                    OnPropertyChanged(() => SearchProperty);

                }
            }
        }

        public string? OrderProperty
        {
            get => Service.OrderProperty;
            set
            {
                if(Service.OrderProperty != value)
                {
                    Service.OrderProperty = value;
                    OnPropertyChanged(() => OrderProperty);
                }
            }
        }

        public bool OrderAscending
        {
            get => Service.OrderAscending;
            set
            {
                if (Service.OrderAscending != value)
                {
                    Service.OrderAscending = value;
                    OnPropertyChanged(() => OrderAscending);
                }
            }
        }

        private DtoType? _SelectedModel;
        public DtoType? SelectedModel
        {
            get => _SelectedModel;
            set
            {
                if (_SelectedModel != value)
                {
                    _SelectedModel = value;
                    OnPropertyChanged(() => SelectedModel);
                    if (SelectedModel != null)
                    {
                        HandleSelect();
                    }
                }
            }
        }
        
        public BaseManyViewModel(string displayName) : base(displayName)
        {
            _Models = default!;
            YesNoOptions = new()
            {
                new YesNoComboBoxItemDto()
                {
                    SelectedOption = YesNoEnum.NoFilter,
                    OptionName = "No filter"
                },
                new YesNoComboBoxItemDto()
                {
                    SelectedOption = YesNoEnum.Yes,
                    OptionName = "Yes"
                },
                new YesNoComboBoxItemDto()
                {
                    SelectedOption = YesNoEnum.No,
                    OptionName = "No"
                }
                
            };
            LargerSmallerOptions = new()
            {
                new LargerSmallerComboBoxItemDto()
                {
                    SelectedOption = LargerSmallerEnum.Larger,
                    OptionName = ">="
                },
                new LargerSmallerComboBoxItemDto()
                {
                    SelectedOption = LargerSmallerEnum.Smaller,
                    OptionName = "<="
                },
                new LargerSmallerComboBoxItemDto()
                {
                    SelectedOption = LargerSmallerEnum.Equal,
                    OptionName = "=="
                },
                new LargerSmallerComboBoxItemDto()
                {
                    SelectedOption = LargerSmallerEnum.NoFilter,
                    OptionName = " ",
                }
            };
            SearchComboBoxDto = Service.GetSearchComboBoxDtos();
            SearchProperty = SearchComboBoxDto.FirstOrDefault()?.PropertyTitle;
             OrderByComboBoxDto = Service.GetOrderByComboBoxDtos();
            OrderProperty = OrderByComboBoxDto.FirstOrDefault()?.PropertyTitle;

            Refresh();
            RefreshCommand = new BaseCommand(() => Refresh());
            DeleteCommand = new BaseCommand(() => Delete());
            CreateNewCommand = new BaseCommand(() => CreateNew());
            FilterCommand = new BaseCommand(() => Refresh());
            EditCommand = new BaseCommand(() => Edit());
            ClearFiltersCommand = new BaseCommand(() => ClearFilter());
        }
        protected void Refresh()
        {
            Models = new ObservableCollection<DtoType>(Service.GetModels());
        }
        private void Delete()
        {
            if (SelectedModel != null)
            {
                Service.DeleteModel(SelectedModel);
                Models.Remove(SelectedModel);
            }
        }
        protected abstract void CreateNew();

        protected abstract void Edit();
      
        protected virtual void HandleSelect()
        { 
        }
        protected abstract void ClearFilter();

    }
}
