﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.Models.Dtos
{
    public class PublisherDto
    {
        public int Id { get; set; }
   
        public string Name { get; set; } = null!;
        public string? Nip { get; set; }

        public string? Address { get; set; }
    }
}
