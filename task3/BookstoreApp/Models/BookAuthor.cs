using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.Models;

[PrimaryKey("BookId", "AuthorId")]
public partial class BookAuthor
{
    [Key]
    public int BookId { get; set; }

    [Key]
    public int AuthorId { get; set; }

    [Column("isActive")]
    public bool IsActive { get; set; }

    [ForeignKey("AuthorId")]
    [InverseProperty("BookAuthors")]
    public virtual Author Author { get; set; } = null!;

    [ForeignKey("BookId")]
    [InverseProperty("BookAuthors")]
    public virtual Book Book { get; set; } = null!;
}
