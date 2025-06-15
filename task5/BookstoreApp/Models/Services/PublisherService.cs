using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Helpers;
using BookstoreApp.Models.Contexts;
using BookstoreApp.Models.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace BookstoreApp.Models.Services
{
    public class PublisherService : BaseService<PublisherDto, Publisher>
    {
        public YesNoEnum HasAddress { get; set; }
        public YesNoEnum HasNip { get; set; }

        public PublisherService()
        {
            HasAddress = YesNoEnum.NoFilter;
            HasNip = YesNoEnum.NoFilter;
        }
        public override void AddModel(Publisher model)
        {
            DatabaseContext.Publishers.Add(model);
            DatabaseContext.SaveChanges();
        }

        public override void DeleteModel(PublisherDto model)
        {
            Publisher publisher = DatabaseContext.Publishers.First(item => item.Id == model.Id);
            publisher.IsActive = false;
            publisher.DeleteDateTime = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override Publisher GetModel(int id)
        {
            return DatabaseContext.Publishers.First(item => item.Id == id);
        }

        public override List<PublisherDto> GetModels()
        {
            IQueryable<Publisher> publishers = DatabaseContext.Publishers
                                                                    .Where(item => item.IsActive);


            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(Publisher.Name):
                        publishers = publishers.Where(item =>
                         item.Name.ToLower().Contains(SearchInput.ToLower()));
                        break;
                    case nameof(Publisher.Nip):
                        publishers = publishers.Where(item => item.Nip.Contains(SearchInput));
                        break;
                }
            }

            
            switch (HasAddress)
            {
                case YesNoEnum.Yes:
                    publishers = publishers.Where(item=> !string.IsNullOrEmpty(item.Address));
                    break;
                case YesNoEnum.No:
                    publishers = publishers.Where(item => string.IsNullOrEmpty(item.Address));
                    break;
            }

            switch (HasNip)
            {
                case YesNoEnum.Yes:
                    publishers = publishers.Where(item => !string.IsNullOrEmpty(item.Nip));
                    break;
                case YesNoEnum.No:
                    publishers = publishers.Where(item => string.IsNullOrEmpty(item.Nip));
                    break;
            }

            switch (OrderProperty)
            {
                case nameof(Publisher.Name):
                    publishers = OrderAscending ? publishers.OrderBy(item => item.Name) : publishers.OrderByDescending(item => item.Name);
                    break;
                case nameof(Publisher.Nip):
                    publishers = OrderAscending ? publishers.OrderBy(item => item.Nip) : publishers.OrderByDescending(item => item.Nip);
                    break;
            }
            
            IQueryable<PublisherDto> publishersDto = publishers.Select(item => new PublisherDto
            {
                Id = item.Id,
                Name = item.Name,
                Nip = item.Nip,
                Address = ""

            });
            return publishersDto.ToList();
        }

        public override Publisher CreateModel()
        {
            return new Publisher()
            {
                IsActive = true,
                CreationDateTime = DateTime.Now,
            };
        }

        public override void UpdateModel(Publisher model)
        {
            model.EditDateTime = DateTime.Now;
            DatabaseContext.Publishers.Update(model);
            DatabaseContext.SaveChanges();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto
                {
                    PropertyTitle = nameof(Publisher.Name),
                    DisplayName = "Name",
                },
                new SearchComboBoxDto
                {
                    PropertyTitle = nameof(Publisher.Nip),
                    DisplayName = "NIP",
                },
            };
        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto
                {
                    PropertyTitle = nameof(Publisher.Name),
                    DisplayName = "Name",
                },
                new SearchComboBoxDto
                {
                    PropertyTitle = nameof(Publisher.Nip),
                    DisplayName = "NIP",
                },
            };
        }

        public override string ValidateProperty(string columnName, Publisher model)
        {
            if (columnName == nameof(Publisher.Name))
            {
                if (model.Name.IsNullOrEmpty())
                    return "Title is required";
            }
            if (columnName == nameof(Publisher.Nip))
            {
                if (!model.Nip.IsNullOrEmpty() && !int.TryParse(model.Nip, out int a))
                {
                    return "Nip must be numeric";
                }
            }
            return string.Empty;
        }
    }
}
