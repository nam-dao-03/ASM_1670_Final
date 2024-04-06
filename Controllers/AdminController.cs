using Microsoft.AspNetCore.Mvc;

namespace ASM_1670_Final.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
