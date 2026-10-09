using Microsoft.AspNetCore.Mvc;

namespace ignou.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
