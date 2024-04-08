using ASM_1670_Final.Data;
using ASM_1670_Final.Models;
using ASM_1670_Final.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASM_1670_Final.Controllers
{
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ClientController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
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
        [NonAction]
        private string UploadedFile(Job model)
        {
            string uniqueFileName = null;
            if (model.Avatar != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = model.Avatar.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Avatar.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        [HttpPost]
        public IActionResult CreateJob(Job model)
        {
            string uniqueFileName = UploadedFile(model);
            Job job = new Job
            {
                JobTitle = model.JobTitle,
                Location = model.Location,
                Industry = model.Industry,
                AvartarUrl = uniqueFileName,
                Description = model.Description,
                Requiered1 = model.Requiered1,
                Requiered2 = model.Requiered2,
                ApplicationDeadline = model.ApplicationDeadline,
                LowestPrice = model.LowestPrice,
                HighestPrice = model.HighestPrice,
                UserId = model.UserId,
            };
            _context.Jobs.Add(job);
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
