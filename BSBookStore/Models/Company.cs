using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BSBookStore.Models
{
  public class Company
  {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public bool IsAuthenticated { get; set; }
  }
}
