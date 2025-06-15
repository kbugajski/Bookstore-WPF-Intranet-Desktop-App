using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            DatabaseContext.Addresses.Add(model);
            DatabaseContext.SaveChanges();
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
            return DatabaseContext.Addresses.First(item => item.Id == id);
        }

        public override List<AddressDto> GetModels()
        {
            IQueryable<Address> addresss = DatabaseContext.Addresses.Include(item => item.Country)
                                                                    .Where(item => item.IsActive);


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
            DatabaseContext.Addresses.Update(model);
            DatabaseContext.SaveChanges();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropteryTitle = nameof(Address.Country),
                    DisplayName = "By Country"
                },
                new SearchComboBoxDto()
                {
                    PropteryTitle = nameof(Address.PostalCode),
                    DisplayName = "By Postal Code"
                },
                new SearchComboBoxDto()
                {
                    PropteryTitle = nameof(Address.Street),
                    DisplayName = "By Street"
                },
            };
        }
    }
}
