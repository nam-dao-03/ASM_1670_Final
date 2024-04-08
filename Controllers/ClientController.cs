using ASM_1670_Final.Data;
using ASM_1670_Final.Models;
using ASM_1670_Final.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASM_1670_Final.Controllers
{
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ClientController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var listJob = _context.Jobs.ToList();
            var user = _userManager.GetUserId(User);
            ViewBag.UserId = user;
            return View(listJob);
        }
        [HttpGet]
        public IActionResult CreateJob()
        {
            var user = _userManager.GetUserId(User);
            if (user != null)
            {
                ViewBag.UserId = user;
                return View();
            } else
            {
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public IActionResult CreateJob(Job model)
        {
            _context.Jobs.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            
        }
        [HttpGet]
        public IActionResult DetailJob(int? id)
        {
            ViewBag.JobId = id;
            var job = _context.Jobs.Find(id);
            var jobs = _context.Jobs.ToList();
            var viewmodel = new ViewModelDetailJob
            {
                JobList = jobs,
                job = job
            };
            return View(viewmodel);
        }
        [HttpGet]
        public IActionResult EditJob(int? id)
        {
            if (id != null)
            {
                var editJob = _context.Jobs.Find(id);
                return View(editJob);   
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult EditJob(Job job)
        {
            _context.Jobs.Update(job);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult DeleteJob(int? id)
        {
            if (id != null)
            {
                var deleteJob = _context.Jobs.Find(id);
                return View(deleteJob);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult DeleteJob(Job job)
        {
            _context.Jobs.Remove(job);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
