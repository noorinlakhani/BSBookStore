using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSBookStore.Models
{
  public class BookVM
  {
    public Book Book { get; set; }
    public IEnumerable<SelectListItem> CategoryList { get; set; }
  }
}
