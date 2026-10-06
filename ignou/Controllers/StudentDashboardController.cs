using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ignou.Controllers
{
    [Authorize]
    public class StudentDashboardController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}