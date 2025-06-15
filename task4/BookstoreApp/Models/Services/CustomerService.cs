using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Helpers;
using BookstoreApp.Models.Dtos;
using Microsoft.EntityFrameworkCore;

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
            return DatabaseContext.Customers.First(item => item.Id == id);
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
                    case nameof(Customer.Title):
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

            switch (HasCode)
            {
                case YesNoEnum.Yes:
                    customers = customers.Where(item => !string.IsNullOrEmpty(item.Code));
                    break;
                case YesNoEnum.No:
                    customers = customers.Where(item => string.IsNullOrEmpty(item.Code));
                    break;
            }
            switch(HasNip)
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
                Title = $"{item.FirstName.Trim()} {item.LastName.Trim()}",
                CustomerCategory = item.CustomerCategory.Title,
                CustomerStatus = item.CustomerStatus.Title,
                CountryName = item.Address.Country.Title,
                Address =  $"{ item.Address.Street} {item.Address.HouseNumber} {item.Address.PostalCode} {item.Address.PostalCity}",

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
                }
            };
        }

        public override void UpdateModel(Customer model)
        {
            DatabaseContext.Customers.Update(model);
            DatabaseContext.SaveChanges();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropteryTitle = nameof(Customer.FirstName),
                    DisplayName = "By Names"
                },
                new SearchComboBoxDto()
                {
                    PropteryTitle = nameof(Customer.Nip),
                    DisplayName = "By Nip"
                },
                new SearchComboBoxDto()
                {
                    PropteryTitle = nameof(Customer.Code),
                    DisplayName = "By Code"
                },
            };
        }
    }
}
