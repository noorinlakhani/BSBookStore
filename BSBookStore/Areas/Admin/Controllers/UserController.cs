using BSBookStore.Data;
using BSBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSBookStore.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Authorize(Roles = StaticData.Role_Admin + "," + StaticData.Role_Employee)]
  public class UserController : Controller
  {
    private readonly ApplicationDbContext _db;

    public UserController(ApplicationDbContext db)
    {
      _db = db;
    }
    public IActionResult Index()
    {
      var userList = _db.ApplicationUsers.Include(u => u.Company).ToList();
      var roleList = _db.Roles.ToList();
      var userRoleList = _db.UserRoles.ToList();
      foreach (var user in userList)
      {
        var roleId = userRoleList.FirstOrDefault(u => u.UserId == user.Id).RoleId;
        user.Role = roleList.FirstOrDefault(u => u.Id == roleId).Name;
        if(user.Company == null)
        {
          user.Company = new Models.Company()
          {
            Name = " "
          };
        }
      }
      return View(userList);
    }
  }
}
