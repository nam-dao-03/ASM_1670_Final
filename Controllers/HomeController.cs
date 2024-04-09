using ASM_1670_Final.Data;
using ASM_1670_Final.Models;
using ASM_1670_Final.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASM_1670_Final.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var jobs = _context.Jobs.ToList();
            return View(jobs);
        }
        [HttpGet]
        public IActionResult DetailJob(int? id)
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
        [HttpPost]
        public IActionResult Search(string SearchJob, string SearchLocation)
        {
            var jobSearch = _context.Jobs.ToList();
            if(!String.IsNullOrEmpty(SearchJob) && String.IsNullOrEmpty(SearchLocation))
            {
                jobSearch = jobSearch.Where(n => n.JobTitle.Contains(SearchJob)).ToList();
            }
            else if(!String.IsNullOrEmpty(SearchLocation) && String.IsNullOrEmpty(SearchJob))
            {
                jobSearch = jobSearch.Where(n => n.Location.Contains(SearchLocation)).ToList();
            }
            else if(!String.IsNullOrEmpty(SearchLocation) && !String.IsNullOrEmpty(SearchJob))
            {
                jobSearch = jobSearch.Where(n => n.JobTitle.Contains(SearchJob) && n.Location.Contains(SearchLocation)).ToList();
            } else
            {
                return View(jobSearch);
            }
            return View(jobSearch); 
        }
        [HttpGet]
        public IActionResult MyCV(int id)
        {
            ViewBag.JobId = id;
            return View();
        }
        [HttpPost]
        public IActionResult CreateCV(int id)
        {
            ViewBag.JobId = id;
            return View();
        }
    }
}
