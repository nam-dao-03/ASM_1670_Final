using ASM_1670_Final.Data;
using ASM_1670_Final.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM_1670_Final.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
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
        [HttpPost]
        public IActionResult CreateRole(ApplicationRole model)
        {
            if (!_context.Roles.Any(e => e.Name == model.Name))
            {
                _context.Roles.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexRole));
            }
            else
            {
                return View();
            }
        }
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

        [HttpGet]
        public IActionResult IndexUser()
        {
            var users = _context.UserRoles.Include(x => x.User).Include(y => y.Role).ToList();
            return View(users);
        }
    }
}
