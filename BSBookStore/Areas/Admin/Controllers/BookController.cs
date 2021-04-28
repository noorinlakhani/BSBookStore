using BSBookStore.Data;
using BSBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSBookStore.Controllers
{
  [Area("Admin")]
  [Authorize(Roles = StaticData.Role_Admin)]
  public class BookController : Controller
  {
    private readonly ApplicationDbContext _db;

    public BookController(ApplicationDbContext db)
    {
      _db = db;
    }
    public IActionResult Index()
    {
      IEnumerable<Book> bookList = _db.Books.Include("Category").ToList();
      return View(bookList);
    }
    public IActionResult Create()
    {
      BookVM bookVM = new BookVM()
      {
        Book = new Book(),
        CategoryList = _db.Categories.Select(i =>
          new SelectListItem()
          {
            Text = i.Name,
            Value = i.Id.ToString()
          })
      };
      return View(bookVM);
    }
    [HttpPost]
    public IActionResult Create(BookVM bookVM)
    {
      if (ModelState.IsValid)
      {
        _db.Books.Add(bookVM.Book);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      return NotFound();
    }
    public IActionResult Edit(int? id)
    {
      if (id == 0 || id == null)
        return NotFound();
      BookVM bookVM = new BookVM()
      {
        Book = new Book(),
        CategoryList = _db.Categories.Select(i =>
          new SelectListItem()
          {
            Text = i.Name,
            Value = i.Id.ToString()
          })
      };
      bookVM.Book = _db.Books.Find(id);
      if(bookVM.Book ==null)
        return NotFound();
      return View(bookVM);
    }
    [HttpPost]
    public IActionResult Edit(Book book)
    {
      if(ModelState.IsValid)
      {
        _db.Books.Update(book);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View();
    }
    public IActionResult Delete(int? id)
    {
      if (id == 0 || id == null)
        return NotFound();
      BookVM bookVM = new BookVM()
      {
        Book = new Book(),
        CategoryList = _db.Categories.Select(i =>
          new SelectListItem()
          {
            Text = i.Name,
            Value = i.Id.ToString()
          })
      };
      bookVM.Book = _db.Books.Find(id);
      if (bookVM.Book == null)
        return NotFound();
      return View(bookVM);
    }
    [HttpPost]
    public IActionResult Delete(Book book)
    {
      if (ModelState.IsValid)
      {
        _db.Books.Remove(book);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View();
    }
  }
}
