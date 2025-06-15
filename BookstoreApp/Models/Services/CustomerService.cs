using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BookstoreApp.Helpers;
using BookstoreApp.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookstoreApp.Models.Services
{
    public class CustomerService : BaseService<CustomerDto, Customer>
    {
        public YesNoEnum HasCode { get; set; }
        public YesNoEnum HasNip { get; set; }
        public override void AddModel(Customer model)
        {
            DatabaseContext.Customers.Add(model);
            DatabaseContext.SaveChanges();
        }

        public CustomerService()
        {
            HasCode = YesNoEnum.NoFilter;
            HasNip = YesNoEnum.NoFilter;
        }


        public override void DeleteModel(CustomerDto model)
        {
            Customer customer = DatabaseContext.Customers.First(item => item.Id == model.Id);
            customer.IsActive = false;
            customer.DeleteDateTime = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override Customer GetModel(int id)
        {
            return DatabaseContext.Customers.Include(item => item.Address).ThenInclude(item => item.Country).First(item => item.Id == id);
        }

        public override List<CustomerDto> GetModels()
        {
            IQueryable<Customer> customers = DatabaseContext.Customers.Include(item => item.Address).Include(item => item.CustomerCategory).
                                                            Include(item => item.CustomerStatus)
                                                                //.ThenInclude(item => item.Country)
                                                                .Where(item => item.IsActive);

            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(Customer.FirstName):
                        customers = customers.Where(item =>
                         item.FirstName.ToLower().Contains(SearchInput.ToLower()) ||
                         item.LastName.ToLower().Contains(SearchInput.ToLower()));
                        break;
                    case nameof(Customer.Nip):
                        customers = customers.Where(item => item.Nip.Contains(SearchInput));
                        break;
                    case nameof(Customer.Code):
                        customers = customers.Where(item => item.Code.Contains(SearchInput));
                        break;
                }
            }

            switch (OrderProperty)
            {
                case nameof(Customer.LastName):
                    customers = OrderAscending ? customers.OrderBy(item => item.LastName) : customers.OrderByDescending(item => item.LastName);
                    break;
                case nameof(Customer.Nip):
                    customers = OrderAscending ? customers.OrderBy(item => item.Nip) : customers.OrderByDescending(item => item.Nip);
                    break;
                case nameof(Customer.Code):
                    customers = OrderAscending ? customers.OrderBy(item => item.Code) : customers.OrderByDescending(item => item.Code);
                    break;
                case nameof(Customer.Title):
                    customers = OrderAscending ? customers.OrderBy(item => item.Title) : customers.OrderByDescending(item => item.Title);
                    break;

            }


            switch (HasCode)
            {
                case YesNoEnum.Yes:
                    customers = customers.Where(item => !string.IsNullOrEmpty(item.Code));
                    break;
                case YesNoEnum.No:
                    customers = customers.Where(item => string.IsNullOrEmpty(item.Code));
                    break;
            }
            switch (HasNip)
            {
                case YesNoEnum.Yes:
                    customers = customers.Where(item => !string.IsNullOrWhiteSpace(item.Nip));
                    break;
                case YesNoEnum.No:
                    customers = customers.Where(item => string.IsNullOrWhiteSpace(item.Nip));
                    break;
            }

            IQueryable<CustomerDto> customersDto = customers.Select(item => new CustomerDto()
            {
                Id = item.Id,
                Code = item.Code,
                Nip = item.Nip,
                Title = item.Title,
                FirstName = item.FirstName.Trim(),
                LastName = item.LastName.Trim(),
                CustomerCategory = item.CustomerCategory.Title,
                CustomerStatus = item.CustomerStatus.Title,
                CountryName = item.Address.Country.Title,
                Address = $"{item.Address.Street} {item.Address.HouseNumber} {item.Address.PostalCode} {item.Address.PostalCity}",

            });

            
                
            return customersDto.ToList();
        }

        public override Customer CreateModel()
        {
            return new Customer()
            {
                IsActive = true,
                CreationDateTime = DateTime.Now,
                Address = new()
                {
                    IsActive = true,
                    CreationDateTime = DateTime.Now
                },
                CustomerCategoryId = DatabaseContext.CustomerCategories.Select(item => item.Id).ToList().FirstOrDefault(),
                CustomerStatusId = DatabaseContext.CustomerStatuses.Select(item => item.Id).ToList().FirstOrDefault(),
            };
        }

        public override void UpdateModel(Customer model)
        {
            model.EditDateTime = DateTime.Now;
            DatabaseContext.Customers.Update(model);
            DatabaseContext.SaveChanges();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                //new SearchComboBoxDto()
                //{
                //    PropertyTitle = nameof(Customer.FirstName),
                //    DisplayName = "By First Name"
                //},
                //new SearchComboBoxDto()
                //{
                //    PropertyTitle = nameof(Customer.LastName),
                //    DisplayName = "By Last Name"
                //},
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Customer.FirstName),
                    DisplayName = "Names"
                },

                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Customer.Title),
                    DisplayName = "Title"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Customer.Nip),
                    DisplayName = "Nip"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Customer.Code),
                    DisplayName = "Code"
                },
            };
        }
        

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                //new SearchComboBoxDto()
                //{
                //    PropertyTitle = nameof(Customer.FirstName),
                //    DisplayName = "By First Name"
                //},
                //new SearchComboBoxDto()
                //{
                //    PropertyTitle = nameof(Customer.LastName),
                //    DisplayName = "By Last Name"
                //},
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Customer.LastName),
                    DisplayName = "Last Name"
                },

                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Customer.Title),
                    DisplayName = "Title"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Customer.Nip),
                    DisplayName = "Nip"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Customer.Code),
                    DisplayName = "Code"
                },
            };
        }
        public override string ValidateProperty(string columnName, Customer model)
        {
            if (columnName == nameof(Customer.LastName))
            {
                if (string.IsNullOrWhiteSpace(model.LastName))
                    return "Last Name is required";

                if (!Regex.IsMatch(model.LastName, @"^[a-zA-Z\s-]+$"))
                    return "Last Name can only contain letters, spaces, and hyphens";
            }

            if (columnName == nameof(Customer.FirstName))
            {
                if (string.IsNullOrWhiteSpace(model.FirstName))
                    return "First Name is required";

                if (!Regex.IsMatch(model.FirstName, @"^[a-zA-Z\s-]+$"))
                    return "First Name can only contain letters, spaces, and hyphens";
            }
            if (columnName == nameof(Customer.Title))
            {
                if (model.Title.IsNullOrEmpty())
                    return "Title is required";
            }
            if (columnName == nameof(Customer.Code))
            {
                if (!model.Code.IsNullOrEmpty() && !int.TryParse(model.Code, out int a))
                {
                    return "Code must be numeric";
                }
            }
            if (columnName == nameof(Customer.Nip))
            {
                if (!model.Nip.IsNullOrEmpty() && !int.TryParse(model.Nip, out int a))
                {
                    return "Nip must be numeric";
                }
            }
            if (columnName == nameof(Customer.Address.PhoneNumber))
            {
                if (!string.IsNullOrWhiteSpace(model.Address?.PhoneNumber))
                {
                    // Obsługuje formaty międzynarodowe i lokalne np. "+48 123 456 789", "123-456-789", "(123) 456-7890"
                    if (!Regex.IsMatch(model.Address.PhoneNumber, @"^\+?\d{1,3}?[-. \(\)]?\d{1,4}[-. \(\)]?\d{1,4}[-. \(\)]?\d{1,9}$"))
                        return "Invalid phone number format.\nUse one of these: '+## ### ### ###' or '###-###-###' or '(###) ###-####'";
                }
            

            }
            if (columnName == nameof(Customer.Address.PostalCode))
            {
                if (!string.IsNullOrWhiteSpace(model.Address?.PostalCode) && !Regex.IsMatch(model.Address?.PostalCode, @"^\d{2}-\d{3}$"))
                    return "Invalid postal code format.\nCorrect format: '##-###'";
            }
            if (columnName == nameof(Customer.Address.HouseNumber))
            {
                if (!string.IsNullOrWhiteSpace(model.Address?.HouseNumber) && !int.TryParse(model.Address?.HouseNumber, out int a))
                    return "House number must be numeric";
            }
            if (columnName == nameof(Customer.Address.FlatNumber))
            {
                if (!string.IsNullOrWhiteSpace(model.Address?.FlatNumber) && !int.TryParse(model.Address?.FlatNumber, out int a))
                    return "Flat number must be numeric";
            }


            return string.Empty;
        }
    }

}

