using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.Models;

[PrimaryKey("BookId", "CategoryId")]
public partial class BookCategory
{
    [Key]
    public int BookId { get; set; }

    [Key]
    public int CategoryId { get; set; }

    [Column("isActive")]
    public bool IsActive { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("BookCategories")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("CategoryId")]
    [InverseProperty("BookCategories")]
    public virtual Category Category { get; set; } = null!;
}
