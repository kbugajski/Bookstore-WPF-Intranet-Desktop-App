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


            IQueryable<PublisherDto> publishersDto = publishers.Select(item => new PublisherDto
            {
                Id = item.Id,
                Name = item.Name,
                Nip = item.Nip,
                Address = item.Address

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
            DatabaseContext.Publishers.Update(model);
            DatabaseContext.SaveChanges();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            throw new NotImplementedException();
        }
    }
}
