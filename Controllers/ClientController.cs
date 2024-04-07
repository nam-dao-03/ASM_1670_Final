using ASM_1670_Final.Data;
using ASM_1670_Final.Models;
using ASM_1670_Final.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ASM_1670_Final.Controllers
{
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ClientController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DetailJob(string? id)
        {
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
        public IActionResult CreateJob()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateJob(Job model)
        {
            _context.Jobs.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            
        }
    }
}
