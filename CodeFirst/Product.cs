using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst
{
   public class Product
   {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [Column(TypeName ="varchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }

        //[NotMapped]

        [Precision(15,3)]
        public decimal UnitPrice { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }


    }
}
