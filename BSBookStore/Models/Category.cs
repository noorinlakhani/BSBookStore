using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BSBookStore.Models
{
  public class Category
  {
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(20)]
    public string Name { get; set; }
  }
}
