using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ignou.Controllers
{

    
    public class AdminDashboardController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
