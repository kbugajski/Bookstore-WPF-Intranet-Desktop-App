using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.Models;

public partial class Review
{
    [Key]
    public int Id { get; set; }

    public int BookId { get; set; }

    public int CustomerId { get; set; }

    public double Rating { get; set; }

    public string? ReviewText { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ReviewDate { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreationDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EditDateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeleteDateTime { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("Reviews")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("CustomerId")]
    [InverseProperty("Reviews")]
    public virtual Customer Customer { get; set; } = null!;
}
