using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Helpers;
using BookstoreApp.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BookstoreApp.Models.Services
{
    public class InvoiceService : BaseService<InvoiceDto, Invoice>
    {
        public DateTime? DateOfIssueFrom { get; set; }
        public DateTime? DateOfIssueTo { get; set; }
        public bool IsPaid {  get; set; }
        

        public override void AddModel(Invoice model)
        {
            DatabaseContext.Invoices.Add(model);
            DatabaseContext.SaveChanges();
        }

        public override void DeleteModel(InvoiceDto model)
        {
            Invoice invoice = DatabaseContext.Invoices.First(item => item.Id == model.Id);
            invoice.IsActive = false;
            invoice.DeleteDateTime = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override Invoice GetModel(int id)
        {
            return DatabaseContext.Invoices.Include(item => item.Customer).First(item => item.Id == id);
        }

        public override List<InvoiceDto> GetModels()
        {
            IQueryable<Invoice> invoices = DatabaseContext.Invoices.Include(item => item.Customer).Include(item => item.PaymentMethod)
                                                                    .Where(item => item.IsActive);

            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(Invoice.Customer):
                        invoices = invoices.Where(item =>
                         item.Customer.FirstName.ToLower().Contains(SearchInput.ToLower()) ||
                         item.Customer.LastName.ToLower().Contains(SearchInput.ToLower()));
                        break;
                    case nameof(Invoice.Number):
                        invoices = invoices.Where(item => item.Number.Contains(SearchInput));
                        break;
                    case nameof(Invoice.PaymentMethod):
                        invoices = invoices.Where(item => item.PaymentMethod.Title.Contains(SearchInput));
                        break;
                }
            }

            switch (OrderProperty)
            {
                case nameof(Invoice.Customer):
                    invoices = OrderAscending ? invoices.OrderBy(item => item.Customer.LastName) : invoices.OrderByDescending(item => item.Customer.LastName);
                    break;
                case nameof(Invoice.Number):
                    invoices = OrderAscending ? invoices.OrderBy(item => item.Number) : invoices.OrderByDescending(item => item.Number);
                    break;
                case nameof(Invoice.PaymentMethod):
                    invoices = OrderAscending ? invoices.OrderBy(item => item.PaymentMethod) : invoices.OrderByDescending(item => item.PaymentMethod.Title);
                    break;
              
            }

            


            if (IsPaid)
            {
                invoices = invoices.Where(item => item.IsPaid);
            }
            if(DateOfIssueFrom != null)
            {
                invoices = invoices.Where(item => item.DateOfIssue >= DateOfIssueFrom);
            }
            if (DateOfIssueTo != null)
            {
                invoices = invoices.Where(item => item.DateOfIssue <= DateOfIssueFrom);
            }
            IQueryable<InvoiceDto> invoicesDto = invoices.Select(item => new InvoiceDto
            {
                Id = item.Id,
                PaymentDate = item.PaymentDate,
                CustomerName = $"{item.Customer.FirstName.Trim()} {item.Customer.LastName.Trim()}",
                Number = item.Number,
                IsPaid = item.IsPaid,
                DateOfIssue = item.DateOfIssue,
                PaymentMethodName = item.PaymentMethod.Title


            });
            return invoicesDto.ToList();
        }

        public override Invoice CreateModel()
        {
            return new Invoice()
            {
                IsActive = true,
                CreationDateTime = DateTime.Now,
            };
        }

        public override void UpdateModel(Invoice model)
        {
            model.EditDateTime = DateTime.Now;
            DatabaseContext.Invoices.Update(model);
            DatabaseContext.SaveChanges();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto(){
                    PropertyTitle = nameof(Invoice.Number),
                    DisplayName = "Number"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle= nameof(Invoice.Customer),
                    DisplayName = "Customer"
                },
                new SearchComboBoxDto(){
                    PropertyTitle= nameof(Invoice.PaymentMethod),
                    DisplayName = "Payment Method"
                }
            };
        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto(){
                    PropertyTitle = nameof(Invoice.Number),
                    DisplayName = "Number"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle= nameof(Invoice.Customer),
                    DisplayName = "Customer"
                },
                new SearchComboBoxDto(){
                    PropertyTitle= nameof(Invoice.PaymentMethod),
                    DisplayName = "Payment Method"
                }
            };
        }
        public override string ValidateProperty(string columnName, Invoice model)
        {
            if (columnName == nameof(Invoice.Number))
            {
                if (string.IsNullOrEmpty(model.Number))
                    return "Number is required";
                if (!int.TryParse(model.Number, out int a))
                    return "Number must be numeric";
            }

            return string.Empty;
        }
    }
}
