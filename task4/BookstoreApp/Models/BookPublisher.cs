using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.Models;

[PrimaryKey("BookId", "PublisherId")]
public partial class BookPublisher
{
    [Key]
    public int BookId { get; set; }

    [Key]
    public int PublisherId { get; set; }

    [Column("isActive")]
    public bool IsActive { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("BookPublishers")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("PublisherId")]
    [InverseProperty("BookPublishers")]
    public virtual Publisher Publisher { get; set; } = null!;
}
