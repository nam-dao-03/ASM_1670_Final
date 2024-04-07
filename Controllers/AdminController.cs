using ASM_1670_Final.Data;
using ASM_1670_Final.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASM_1670_Final.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public AdminController(ApplicationDbContext context, RoleManager<ApplicationRole> roleManager )
        {
            _context = context;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Roles
        public IActionResult IndexRole()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult CreateRole(ApplicationRole model)
        //{
        //    if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
        //    {
        //        _roleManager.CreateAsync(new ApplicationRole(model.Name)).GetAwaiter().GetResult();
        //        return RedirectToAction(nameof(IndexRole));
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}
        [HttpGet]
        public IActionResult DeleteRole(string? id)
        {
            var role = _context.Roles.Find(id);
            return View(role);
        }
        [HttpPost]
        public IActionResult DeleteRole(ApplicationRole model)
        {
            _context.Roles.Remove(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(IndexRole));
        }

        //User

        public IActionResult IndexUser()
        {
            var users = _context.UserRoles.Include(x => x.User).Include(y => y.Role).ToList();
            return View(users);
        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            LoadRoles();
            return View();
        }
        [NonAction]
        private void LoadRoles()
        {
            var roles = _context.Roles.ToList();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
        }
        [HttpPost]
        public IActionResult CreateUser(ApplicationUser model, ApplicationUserRole model1)
        { 
            _context.Users.Add(model);
            //ApplicationUserRole UserRole = new ApplicationUserRole
            //{
            //    RoleId = model1.RoleId,
            //    UserId = model.Id
            //};
            //_context.UserRoles.Add(UserRole);
            _context.SaveChanges();
            return RedirectToAction(nameof(IndexUser));
        }
    }
}
