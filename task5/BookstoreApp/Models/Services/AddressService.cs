using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BookstoreApp.Helpers;
using BookstoreApp.Models.Contexts;
using BookstoreApp.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.Models.Services
{
    public class AddressService : BaseService<AddressDto, Address>
    {
        public YesNoEnum HasPhoneNumber { get; set; }
        public override void AddModel(Address model)
        {
            if(model.Id == 0)
            {
                DatabaseContext.Addresses.Add(model);
                DatabaseContext.SaveChanges();
            }
        }

        public AddressService()
        {
            HasPhoneNumber = YesNoEnum.NoFilter;
        }

        public override void DeleteModel(AddressDto model)
        {
            Address address = DatabaseContext.Addresses.First(item => item.Id == model.Id);
            address.IsActive = false;
            address.DeleteDateTime = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override Address GetModel(int id)
        {
            return DatabaseContext.Addresses.Include(item => item.Country).First(item => item.Id == id);
        }

        public override List<AddressDto> GetModels()
        {
            IQueryable<Address> addresss = DatabaseContext.Addresses.Include(item => item.Country)
                                                                    .Where(item => item.IsActive);
            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(Address.Country):
                        addresss = addresss.Where(item => item.Country.Title.ToLower().Contains(SearchInput.ToLower()));
                        break;
                    case nameof(Address.Street):
                        addresss = addresss.Where(item => item.Street.ToLower().Contains(SearchInput.ToLower()));
                        break;
                    case nameof(Address.PostalCity):
                        addresss = addresss.Where(item => item.PostalCity.ToLower().Contains(SearchInput.ToLower()));
                        break;
                    case nameof(Address.PostalCode):
                        addresss = addresss.Where(item => item.PostalCode.ToLower().Contains(SearchInput.ToLower()));
                        break;
                }
            }

            switch (OrderProperty)
            {
                case nameof(Address.Country):
                    addresss = OrderAscending ? addresss.OrderBy(item => item.Country.Title) : addresss.OrderByDescending(item => item.Country.Title);
                    break;
                case nameof(Address.PostalCity):
                    addresss = OrderAscending ? addresss.OrderBy(item => item.PostalCity) : addresss.OrderByDescending(item => item.PostalCity);
                    break;
                case nameof(Address.PostalCode):
                    addresss = OrderAscending ? addresss.OrderBy(item => item.PostalCode) : addresss.OrderByDescending(item => item.PostalCode);
                    break;
                case nameof(Address.Street):
                    addresss = OrderAscending ? addresss.OrderBy(item => item.Street) : addresss.OrderByDescending(item => item.Street);
                    break;
                case nameof(Address.HouseNumber):
                    addresss = OrderAscending ? addresss.OrderBy(item => item.HouseNumber) : addresss.OrderByDescending(item => item.HouseNumber);
                    break;
            }

            switch (HasPhoneNumber)
            {
                case YesNoEnum.Yes:
                    addresss = addresss.Where(item => !string.IsNullOrWhiteSpace(item.PhoneNumber)); 
                    break;
                case YesNoEnum.No:
                    addresss = addresss.Where(item => string.IsNullOrWhiteSpace(item.PhoneNumber)); 
                    break;
            }

            IQueryable<AddressDto> addresssDto = addresss.Select(item => new AddressDto
            {
                Id = item.Id,
                Street = item.Street,
                CountryName = item.Country.Title,
                CountyOrRegion = item.CountyOrRegion,
                FlatNumber = item.FlatNumber,
                HouseNumber = item.HouseNumber,
                PostalCity = item.PostalCity,
                PostalCode = item.PostalCode,
                PhoneNumber = item.PhoneNumber

            });
            return addresssDto.ToList();
        }

        public override Address CreateModel()
        {
            return new Address()
            {
                IsActive = true,
                CreationDateTime = DateTime.Now,
            };
        }

        public override void UpdateModel(Address model)
        {
            model.Country = DatabaseContext.Countries.FirstOrDefault(item => item.Id == model.CountryId);
            model.EditDateTime = DateTime.Now;
            DatabaseContext.Addresses.Update(model);
            DatabaseContext.SaveChanges();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Address.PostalCity),
                    DisplayName = "Postal City"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Address.HouseNumber),
                    DisplayName = "House Number"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Address.Country),
                    DisplayName = "Country"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Address.PostalCode),
                    DisplayName = "Postal Code"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Address.Street),
                    DisplayName = "Street"
                },
            };
        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Address.PostalCity),
                    DisplayName = "Postal City"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Address.HouseNumber),
                    DisplayName = "House Number"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Address.Country),
                    DisplayName = "Country"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Address.PostalCode),
                    DisplayName = "Postal Code"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Address.Street),
                    DisplayName = "Street"
                },
            };
        }
        public override string ValidateProperty(string columnName, Address model)
        {
            if (columnName == nameof(Address.PhoneNumber))
            {
                if (!string.IsNullOrWhiteSpace(model.PhoneNumber))
                {
                    // Obsługuje formaty międzynarodowe i lokalne np. "+48 123 456 789", "123-456-789", "(123) 456-7890"
                    if (!Regex.IsMatch(model.PhoneNumber, @"^\+?\d{1,3}?[-. \(\)]?\d{1,4}[-. \(\)]?\d{1,4}[-. \(\)]?\d{1,9}$"))
                        return "Invalid phone number format.\nUse one of these: '+## ### ### ###' or '###-###-###' or '(###) ###-####'";
                }
                

            }
            if (columnName == nameof(Address.PostalCode))
            {
                if (!string.IsNullOrWhiteSpace(model.PostalCode) && !Regex.IsMatch(model.PostalCode, @"^\d{2}-\d{3}$"))
                    return "Invalid postal code format.\nCorrect format: '##-###'";
            }
            if (columnName == nameof(Address.HouseNumber))
            {
                if (!string.IsNullOrWhiteSpace(model.HouseNumber) && !int.TryParse(model.HouseNumber, out int a))
                    return "House number must be numeric";
            }
            if (columnName == nameof(Address.FlatNumber))
            {
                if (!string.IsNullOrWhiteSpace(model.FlatNumber) && !int.TryParse(model.FlatNumber, out int a))
                    return "Flat number must be numeric";
            }

            return string.Empty;
        }
    }
}
