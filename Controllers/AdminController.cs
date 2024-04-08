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
        public async Task<IActionResult> CreateRole(ApplicationRole model)
        {
            if (!await _roleManager.RoleExistsAsync(model.Name))
            {
                var role = new ApplicationRole { Name = model.Name };
                var result = await _roleManager.CreateAsync(role);
                return RedirectToAction(nameof(IndexRole));
            }
            return View();
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
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
        }
        //[HttpPost]
        //public async Task<IActionResult> CreateUser(ApplicationUser model)
        //{

        //    return View();
        //}

        [HttpGet] 
        public IActionResult DetailUser ()
        {
            var user = _context.Users.ToList();
            return View(user);
        }
        [HttpGet]
        public IActionResult JobList()
        {
            var jobs = _context.Jobs.Include(x => x.Users).ToList();
            return View(jobs);
        }
        [HttpGet]
        public IActionResult DeleteJob(int? id)
        {
            var job = _context.Jobs.Find(id);
            return View(job);
        }
        [HttpPost]
        public IActionResult DeleteJob(Job job)
        {
            _context.Jobs.Remove(job);
            _context.SaveChanges();
            return RedirectToAction(nameof(JobList));
        }
    }
}
