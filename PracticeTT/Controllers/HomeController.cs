using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PracticeTT.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
