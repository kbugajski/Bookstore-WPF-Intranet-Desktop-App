using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Models.Dtos
{
    public class InvoiceDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = null!;

        public string Number { get; set; } = null!;

        public DateTime DateOfIssue { get; set; }

    
        public DateTime PaymentDate { get; set; }

        public string PaymentMethodName { get; set; } = null!;
        public bool IsPaid { get; set; }
    }
}
