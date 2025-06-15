using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Models.Dtos;

namespace BookstoreApp.Models.Services
{
    public class CountryService : BaseService<CountryDto, Country>
    {
        public override void AddModel(Country model)
        {
            DatabaseContext.Countries.Add(model);
            DatabaseContext.SaveChanges();
        }

        public override void DeleteModel(CountryDto model)
        {
            Country country = DatabaseContext.Countries.First(item => item.Id == model.Id);
            country.IsActive = false;
            country.DeleteDateTime = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override Country GetModel(int id)
        {
            return DatabaseContext.Countries.First(item => item.Id == id);
        }

        public override List<CountryDto> GetModels()
        {
            IQueryable<Country> countries = DatabaseContext.Countries
                                                            .Where(item => item.IsActive);
            if (!string.IsNullOrEmpty(SearchInput))
            {
                countries = countries.Where(item => item.Title.Contains(SearchInput));
            }
            IQueryable<CountryDto> countryDto = countries.Select(item => new CountryDto()
            {
                Id = item.Id,
                Title = item.Title
            });
            return countryDto.ToList();
        }

        public override Country CreateModel()
        {
            return new Country()
            {
                IsActive = true,
                CreationDateTime = DateTime.Now
            };
        }

        public override void UpdateModel(Country model)
        {
            DatabaseContext.Countries.Update(model);
            DatabaseContext.SaveChanges();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return null;
        }
    }

}

