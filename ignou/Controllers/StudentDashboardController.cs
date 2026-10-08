using ignou.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ignou.Controllers
{
    [Authorize(Roles ="Student")]
    public class StudentDashboardController : Controller
    {
        private readonly AppDbContext _db;

        public StudentDashboardController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Dashboard()
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier
            );

            if (!int.TryParse(userId, out int id))
            {
                return RedirectToAction("Login", "Account");
            }

            var student = await _db.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (student == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(student);
        }


        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier
            );

            if (!int.TryParse(userId, out int id))
            {
                return RedirectToAction("Login", "Account");
            }

            var student = await _db.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (student == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(student);
        }


        public async Task<IActionResult> Program()
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier
            );

            if (!int.TryParse(userId, out int id))
            {
                return RedirectToAction("Login", "Account");
            }

            var student = await _db.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (student == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(student);
        }


        public async Task<IActionResult> Assignments()
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier
            );

            if (!int.TryParse(userId, out int id))
            {
                return RedirectToAction("Login", "Account");
            }

            var student = await _db.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (student == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(student);
        }

        public async Task<IActionResult> Examination()
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier
            );

            if (!int.TryParse(userId, out int id))
            {
                return RedirectToAction("Login", "Account");
            }

            var student = await _db.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (student == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(student);
        }

        public async Task<IActionResult> Results()
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier
            );

            if (!int.TryParse(userId, out int id))
            {
                return RedirectToAction("Login", "Account");
            }

            var student = await _db.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (student == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(student);
        }

        public async Task<IActionResult> Certificates()
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier
            );

            if (!int.TryParse(userId, out int id))
            {
                return RedirectToAction("Login", "Account");
            }

            var student = await _db.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (student == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(student);
        }

        public async Task<IActionResult> Payments()
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier
            );

            if (!int.TryParse(userId, out int id))
            {
                return RedirectToAction("Login", "Account");
            }

            var student = await _db.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (student == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(student);
        }
    }
}