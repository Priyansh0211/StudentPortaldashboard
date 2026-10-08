using ignou.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ignou.Controllers
{


    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
    {

        private readonly AppDbContext _db;

        public AdminDashboardController(AppDbContext db)
        {
            _db = db; 
        }

        public IActionResult Dashboard()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _db.Users.ToListAsync();
            return View(students);
        }
    }
}
