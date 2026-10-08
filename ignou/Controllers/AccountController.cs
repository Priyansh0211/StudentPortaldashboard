using ignou.Data;
using ignou.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ignou.Controllers
{
    public class AccountController : Controller
    {
        //test
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;

        public AccountController(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        // REGISTER

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check for duplicate Email or Aadhaar Number
            if (_db.Users.Any(u =>
                u.Email == model.Email ||
                u.AadhaarNo == model.AadhaarNo))
            {
                ModelState.AddModelError(
                    "",
                    "An account with this email address or Aadhaar number already exists."
                );

                return View(model);
            }

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                MobileNo = model.MobileNo,

                PasswordHash =
                    BCrypt.Net.BCrypt.HashPassword(model.Password),

                FatherName = model.FatherName,
                DOB = model.DOB,
                Gender = model.Gender,
                Category = model.Category,
                BloodGroup = model.BloodGroup,
                AadhaarNo = model.AadhaarNo,
                MaritalStatus = model.MaritalStatus,
                PersonalDisability = model.PersonalDisability,

                House = model.House,
                City = model.City,
                District = model.District,
                State = model.State,
                Pincode = model.Pincode,

                ProgramType = model.ProgramType,
                ProgramName = model.ProgramName
            };

            _db.Users.Add(user);

            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] =
            "Your student account has been created successfully. Please log in to continue.";
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AdminRegister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminRegister(AdminRegisterViewModel model)
            {
            if (ModelState.IsValid)
            {
                
                if (_db.Users.Any(u => u.Email == model.Email || u.AadhaarNo == model.AadhaarNo))
                {
                    ModelState.AddModelError("", "Email, Aadhaar already exist");
                    return View(model);
                }

                //Auto Generate employee id
                string newEmployeeId = "EMP-101";

                var lastAdmin = _db.Users.Where(u => u.Role == "Admin" && u.EmployeeId != null && u.EmployeeId.StartsWith("EMP-"))
                    .OrderByDescending(u => u.Id).FirstOrDefault();

                if(lastAdmin != null)
                {
                    string lastNumberStr = lastAdmin.EmployeeId.Replace("EMP-", "");
                    if(int.TryParse(lastNumberStr , out int lastNumber))
                    {
                        newEmployeeId = "EMP-" + (lastNumber + 1);
                    }
                }


                var adminUser = new User
                {
                    Role = "Admin", 
                    IsActive = true,

                    
                    FullName = model.FullName,
                    Email = model.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),

                    
                    EmployeeId = newEmployeeId,
                    Department = model.Department,
                    Designation = model.Designation,

                    
                    MobileNo = model.MobileNo,
                    FatherName = model.FatherName,
                    DOB = model.DOB,
                    Gender = model.Gender,
                    BloodGroup = model.BloodGroup,
                    MaritalStatus = model.MaritalStatus,
                    AadhaarNo = model.AadhaarNo,

                    
                    House = model.House,
                    City = model.City,
                    District = model.District,
                    State = model.State,
                    Pincode = model.Pincode

                    
                };

                _db.Users.Add(adminUser);
                await _db.SaveChangesAsync();

                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }

        // LOGIN 

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _db.Users.FirstOrDefault(
                u => u.Email == model.Email
            );

            if (user == null ||
                !BCrypt.Net.BCrypt.Verify(
                    model.Password,
                    user.PasswordHash))
            {
                ModelState.AddModelError(
                    "",
                    "Invalid email address or password."
                );

                return View(model);
            }

            var token = GenerateToken(user);

            Response.Cookies.Append(
                "AuthToken",
                token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(2)
                }
            );

            if (user.Role == "Admin")
            {
                // Agar Admin dashboard banaya hai, toh uska Action aur Controller name yahan daalein
                return RedirectToAction("Dashboard", "AdminDashboard");
            }

            // Default Student ke liye
            return RedirectToAction("Dashboard", "StudentDashboard");
        }

        // LOGOUT

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");

            TempData["SuccessMessage"] =
                "You have been logged out successfully.";

            return RedirectToAction("Login");
        }

        // GENERATE JWT TOKEN

        private string GenerateToken(User user)
        {
            var key = _config["Jwt:Key"];

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key!)
            );

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

            var claims = new[]
            {
    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    new Claim(ClaimTypes.Email, user.Email),
    new Claim(ClaimTypes.Role, user.Role),
    new Claim(ClaimTypes.Name,user.FullName)
};

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}