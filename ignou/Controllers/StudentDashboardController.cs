using ignou.Data;
using ignou.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace ignou.Controllers
{

    [Authorize(Roles = "Student")]
    public class StudentDashboardController : Controller
    {

        private readonly AppDbContext _db;

        public StudentDashboardController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Dashboard()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(userIdString, out int userId))
            {
                var studentDetails = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (studentDetails == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                return View(studentDetails);
            }


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}