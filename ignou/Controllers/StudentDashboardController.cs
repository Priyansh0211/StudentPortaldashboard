using ignou.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ignou.Controllers
{
    [Authorize]
    public class StudentDashboardController : Controller
    {
        private readonly AppDbContext _db;

        public StudentDashboardController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Dashboard()
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier
            );

            var student = _db.Users.FirstOrDefault(
                u => u.Id.ToString() == userId
            );

            if (student == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(student);
        }

        public IActionResult Profile()
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier
            );

            var student = _db.Users.FirstOrDefault(
                u => u.Id.ToString() == userId
            );

            if (student == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(student);
        }

        public IActionResult Program()
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier
            );

            var student = _db.Users.FirstOrDefault(
                u => u.Id.ToString() == userId
            );

            if (student == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(student);
        }
    }
}