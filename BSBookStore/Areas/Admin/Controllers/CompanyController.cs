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
  [Authorize(Roles = StaticData.Role_Admin + "," + StaticData.Role_Employee)]
  public class CompanyController : Controller
  {
    private readonly ApplicationDbContext _db;

    public CompanyController(ApplicationDbContext db)
    {
      _db = db;
    }

    public IActionResult Index()
    {
      IEnumerable<Company> companyList = _db.Companies.ToList();
      return View(companyList);
    }
    public IActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public IActionResult Create(Company company)
    {
      if (ModelState.IsValid)
      {
        _db.Companies.Add(company);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View();
    }
    public IActionResult Edit(int? id)
    {
      if (id == null || id == 0)
        return NotFound();
      Company CategoryFromDb = _db.Companies.Find(id);
      if (CategoryFromDb == null)
        return NotFound();
      return View(CategoryFromDb);
    }
    [HttpPost]
    public IActionResult Edit(Company company)
    {
      if (ModelState.IsValid)
      {
        _db.Companies.Update(company);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View();
    }
    public IActionResult Delete(int? id)
    {
      if (id == null || id == 0)
        return NotFound();
      Company CompanyFromDb = _db.Companies.Find(id);
      if (CompanyFromDb == null)
        return NotFound();
      return View(CompanyFromDb);
    }
    [HttpPost]
    public IActionResult Delete(Company company)
    {
      _db.Companies.Remove(company);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
