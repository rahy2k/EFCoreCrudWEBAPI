// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbFirst.Entities
{
    [Index("CategoryId", Name = "IX_Products_CategoryId")]
    public partial class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; }
        [Required]
        [StringLength(250)]
        [Unicode(false)]
        public string Description { get; set; }
        [Column(TypeName = "decimal(15, 3)")]
        public decimal UnitPrice { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("Products")]
        public virtual Category Category { get; set; }
    }
}