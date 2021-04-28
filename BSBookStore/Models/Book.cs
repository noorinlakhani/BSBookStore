using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BSBookStore.Models
{
  public class Book
  {
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public String Title { get; set; }
    public String Author { get; set; }
    [Range(1,10000)]
    public float Price { get; set; }

    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
  }
}
