using ignou.Data;
using ignou.Models;
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
            var students = await _db.Users.Where(m=>m.Role == "Student").ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> StudentDetails(int id)
        {
            var student = await _db.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _db.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            _db.Users.Remove(student);

            await _db.SaveChangesAsync();

            return RedirectToAction("GetAllStudents");
        }

        [HttpGet]
        public async Task<IActionResult> EditStudent(int id)
        {
            var student = await _db.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            var model = new EditStudentViewModel
            {
                Id = student.Id,

                FullName = student.FullName,
                Email = student.Email,
                MobileNo = student.MobileNo,

                FatherName = student.FatherName,
                DOB = student.DOB,
                Gender = student.Gender,
                Category = student.Category,
                BloodGroup = student.BloodGroup,
                AadhaarNo = student.AadhaarNo,
                MaritalStatus = student.MaritalStatus,
                PersonalDisability = student.PersonalDisability,

                House = student.House,
                City = student.City,
                District = student.District,
                State = student.State,
                Pincode = student.Pincode,

                ProgramType = student.ProgramType,
                ProgramName = student.ProgramName,

                IsActive = student.IsActive
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(int id , EditStudentViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var student = await _db.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            student.FullName = model.FullName;
            student.Email = model.Email;
            student.MobileNo = model.MobileNo;

            student.FatherName = model.FatherName;
            student.DOB = model.DOB;
            student.Gender = model.Gender;
            student.Category = model.Category;
            student.BloodGroup = model.BloodGroup;
            student.AadhaarNo = model.AadhaarNo;
            student.MaritalStatus = model.MaritalStatus;
            student.PersonalDisability = model.PersonalDisability;

            student.House = model.House;
            student.City = model.City;
            student.District = model.District;
            student.State = model.State;
            student.Pincode = model.Pincode;

            student.ProgramType = model.ProgramType;
            student.ProgramName = model.ProgramName;

            student.IsActive = model.IsActive;

            // IMPORTANT:
            // student.PasswordHash ko yahan touch nahi karna hai.

            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Student details updated successfully.";

            return RedirectToAction("StudentDetails", new { id = student.Id });
        }


        [HttpGet]
        public async Task<IActionResult> AddStudent()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddStudent(RegisterViewModel studentDetail)
        {
            if (!ModelState.IsValid)
            {
                return View(studentDetail);
            }

            // Check for duplicate Email or Aadhaar Number
            if (_db.Users.Any(u =>
                u.Email == studentDetail.Email ||
                u.AadhaarNo == studentDetail.AadhaarNo))
            {
                ModelState.AddModelError(
                    "",
                    "An account with this email address or Aadhaar number already exists."
                );

                return View(studentDetail);
            }

            var user = new User
            {
                FullName = studentDetail.FullName,
                Email = studentDetail.Email,
                MobileNo = studentDetail.MobileNo,

                PasswordHash =
                    BCrypt.Net.BCrypt.HashPassword(studentDetail.Password),

                FatherName = studentDetail.FatherName,
                DOB = studentDetail.DOB,
                Gender = studentDetail.Gender,
                Category = studentDetail.Category,
                BloodGroup = studentDetail.BloodGroup,
                AadhaarNo = studentDetail.AadhaarNo,
                MaritalStatus = studentDetail.MaritalStatus,
                PersonalDisability = studentDetail.PersonalDisability,

                House = studentDetail.House,
                City = studentDetail.City,
                District = studentDetail.District,
                State = studentDetail.State,
                Pincode = studentDetail.Pincode,

                ProgramType = studentDetail.ProgramType,
                ProgramName = studentDetail.ProgramName
            };

            _db.Users.Add(user);

            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] =
            "Your student account has been created successfully. Please log in to continue.";
            return RedirectToAction("GetAllStudents","AdminDashboard");
        }
    }
}



