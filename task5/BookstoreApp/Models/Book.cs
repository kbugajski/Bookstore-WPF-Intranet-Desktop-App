using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.Models;

public partial class Book
{
    [Key]
    public int Id { get; set; }

    [StringLength(128)]
    public string Title { get; set; } = null!;

    [Column("ISBN")]
    [StringLength(20)]
    public string Isbn { get; set; } = null!;

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreationDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeleteDateTime { get; set; }

    [InverseProperty("Book")]
    public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();

    [InverseProperty("Book")]
    public virtual ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();

    [InverseProperty("Book")]
    public virtual ICollection<BookPublisher> BookPublishers { get; set; } = new List<BookPublisher>();

    [InverseProperty("Book")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
