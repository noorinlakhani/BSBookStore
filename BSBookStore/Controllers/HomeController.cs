using BSBookStore.Data;
using BSBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BSBookStore.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _db;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
    {
      _logger = logger;
      _db = db;
    }
    
    public IActionResult Index()
    {
      IEnumerable<Book> books = _db.Books.Include("Category").ToList();
      var claimsIdentity = (ClaimsIdentity)User.Identity;
      var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
      if(claim != null)
      {
        var count = _db.ShoppingCart.Where(u => u.ApplicationUserId == claim.Value).ToList().Count();
        HttpContext.Session.SetInt32(StaticData.ssShoppingCart, count);
      }
      return View(books);
    }

    public IActionResult Details(int id)
    {
      Book bk = _db.Books.Include("Category").FirstOrDefault(i => i.Id == id);
      ShoppingCart cart = new ShoppingCart()
      {
        Book = bk,
        BookId = bk.Id
      };
      return View(cart);
    }
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public IActionResult Details(ShoppingCart cartObj)
    {
      cartObj.Id = 0;
      if(ModelState.IsValid)
      {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        cartObj.ApplicationUserId = claim.Value;
        ShoppingCart cartDb = _db.ShoppingCart.Include("Book")
                              .FirstOrDefault(u => u.ApplicationUserId == cartObj.ApplicationUserId && u.BookId == cartObj.BookId);
        if(cartDb == null)
        {
          _db.ShoppingCart.Add(cartObj);
        }
        else
        {
          cartDb.Count += cartObj.Count;
          _db.ShoppingCart.Update(cartDb);
        }
        _db.SaveChanges();

        var count = _db.ShoppingCart.Where(u => u.ApplicationUserId == cartObj.ApplicationUserId).ToList().Count();
        //HttpContext.Session.SetObject(StaticData.ssShoppingCart, cartObj);
        //var obj = HttpContext.Session.GetObject<ShoppingCart>(StaticData.ssShoppingCart);
        HttpContext.Session.SetInt32(StaticData.ssShoppingCart, count);
        return RedirectToAction(nameof(Index));
      }
      else
      {
        Book bk = _db.Books.Include("Category").FirstOrDefault(i => i.Id == cartObj.BookId);
        ShoppingCart cart = new ShoppingCart()
        {
          Book = bk,
          BookId = bk.Id
        };
        return View(cart);
      }
    }
      public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
