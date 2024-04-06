using ASM_1670_Final.Data;
using ASM_1670_Final.Models;
using Microsoft.AspNetCore.Mvc;

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
                return RedirectToAction("IndexRole");
            }
            else
            {
                return View();
            }
        }
        //[HttpPost]
        //public IActionResult DeleteRole(string? id)
        //{
        //    var role = _context.Roles.Find(id);
        //    _context.Remove(role);
        //    _context.SaveChanges();
        //    return RedirectToAction(nameof(IndexRole));
        //}
    }
}
