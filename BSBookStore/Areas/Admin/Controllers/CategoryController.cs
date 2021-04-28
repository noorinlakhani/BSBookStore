using BSBookStore.Data;
using BSBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSBookStore.Controllers
{
  [Area("Admin")]
  [Authorize(Roles = StaticData.Role_Admin)]
  public class CategoryController : Controller
  {
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
      _db = db;
    }

    public IActionResult Index()
    {
      IEnumerable<Category> categoryList = _db.Categories.ToList();
      return View(categoryList);
    }
    public IActionResult Create()
    {       
      return View();
    }
    [HttpPost]
    public IActionResult Create(Category category)
    {
      if(ModelState.IsValid)
      {
        _db.Categories.Add(category);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View();
    }
    public IActionResult Edit(int? id)
    {
      if (id == null || id == 0)
        return NotFound();
      Category CategoryFromDb = _db.Categories.Find(id);
      if(CategoryFromDb == null)
        return NotFound();
      return View(CategoryFromDb);
    }
    [HttpPost]
    public IActionResult Edit(Category category)
    {
      if (ModelState.IsValid)
      {
        _db.Categories.Update(category);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View();
    }
    public IActionResult Delete(int? id)
    {
      if (id == null || id == 0)
        return NotFound();
      Category CategoryFromDb = _db.Categories.Find(id);
      if (CategoryFromDb == null)
        return NotFound();
      return View(CategoryFromDb);
    }
    [HttpPost]
    public IActionResult Delete(Category category)
    {
      
        _db.Categories.Remove(category);
        _db.SaveChanges();
        return RedirectToAction("Index");
     
     
    }
  }
}
