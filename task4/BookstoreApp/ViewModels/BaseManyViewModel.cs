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

namespace BookstoreApp.ViewModels
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
                    SelectedOption = YesNoEnum.Yes,
                    OptionName = "Yes"
                },
                new YesNoComboBoxItemDto()
                {
                    SelectedOption = YesNoEnum.No,
                    OptionName = "No"
                },
                new YesNoComboBoxItemDto()
                {
                    SelectedOption = YesNoEnum.NoFilter,
                    OptionName = "No filter"
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
                    SelectedOption = LargerSmallerEnum.NoFilter,
                    OptionName = " ",
                }
            };


            Refresh();
            RefreshCommand = new BaseCommand(() => Refresh());
            DeleteCommand = new BaseCommand(() => Delete());
            CreateNewCommand = new BaseCommand(() => CreateNew());
            FilterCommand = new BaseCommand(() => Refresh());
            ClearFiltersCommand = new BaseCommand(() => ClearFilter());
        }
        private void Refresh()
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
        protected virtual void HandleSelect()
        { }
        protected abstract void ClearFilter();

    }
}
