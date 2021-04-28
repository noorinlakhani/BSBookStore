using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BSBookStore.Models
{
  public class OrderDetails
  {
    public int Id { get; set; }
    [Required]
    public int OrderId { get; set; }
    [ForeignKey("OrderId")]
    public OrderHeader OrderHeader { get; set; }
    public int BookId { get; set; }
    [ForeignKey("BookId")]
    public Book Book { get; set; }
    public int Count { get; set; }
    public double Price { get; set; }
  }
}
