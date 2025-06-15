using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookstoreApp.Models.Dtos;
using Microsoft.EntityFrameworkCore;

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
            return DatabaseContext.Invoices.First(item => item.Id == id);
        }

        public override List<InvoiceDto> GetModels()
        {
            IQueryable<Invoice> invoices = DatabaseContext.Invoices.Include(item => item.Customer).Include(item => item.Customer).Include(item => item.PaymentMethod)
                                                                    .Where(item => item.IsActive);

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
            DatabaseContext.Invoices.Update(model);
            DatabaseContext.SaveChanges();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            throw new NotImplementedException();
        }
    }
}
