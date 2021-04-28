using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BSBookStore.Models
{
  public class ApplicationUser:IdentityUser
  {
    [Required]
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }

    [NotMapped]
    public string Role { get; set; }

    public int? CompanyId { get; set; }
    [ForeignKey("CompanyId")]
    public Company Company { get; set; }

  }
}
