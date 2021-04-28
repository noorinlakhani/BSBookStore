using BSBookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSBookStore.Data
{
  public class ApplicationDbContext : IdentityDbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Company> Companies { get; set; }

    public DbSet<OrderHeader> OrderHeaders { get; set; }
    public DbSet<OrderDetails> orderDetails { get; set; }
    public DbSet<ShoppingCart> ShoppingCart { get; set; }
  }
}
