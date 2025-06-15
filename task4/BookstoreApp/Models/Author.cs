using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.Models;

[Index("Id", Name = "IX_Authors")]
public partial class Author
{
    [Key]
    public int Id { get; set; }

    [StringLength(128)]
    public string FirstName { get; set; } = null!;

    [StringLength(128)]
    public string LastName { get; set; } = null!;

    public string? Biography { get; set; }

    public bool IsAlive { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreationDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeleteDateTime { get; set; }

    public int? CountryId { get; set; }

    [InverseProperty("Author")]
    public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();

    [ForeignKey("CountryId")]
    [InverseProperty("Authors")]
    public virtual Country? Country { get; set; }
}
