using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BSBookStore.Models
{
  public class ShoppingCart
  {
    public int Id { get; set; }
   
    public string ApplicationUserId { get; set; }

    [ForeignKey("ApplicationUserId")]
    public ApplicationUser ApplicationUser { get; set; }
    public int BookId { get; set; }
    [ForeignKey("BookId")]
    public Book Book { get; set; }
    [Range(1,1000,ErrorMessage ="Please enter value between 1 and 1000")]
    public int Count { get; set; }
    [NotMapped]
    public double Price { get; set; }
    public ShoppingCart()
    {
      Count = 1;
    }
  }
}
