using ignou.Data;
using ignou.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ignou.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;

        public AccountController(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        // --- REGISTER ---
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Email ya Aadhaar duplicate check
                if (_db.Users.Any(u => u.Email == model.Email || u.AadhaarNo == model.AadhaarNo))
                {
                    ModelState.AddModelError("", "Email ya Aadhaar Number pehle se registered hai.");
                    return View(model);
                }

                var user = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    MobileNo = model.MobileNo,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
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

                return RedirectToAction("Login");
            }
            return View(model);
        }


        // --- LOGIN ---
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _db.Users.FirstOrDefault(u => u.Email == model.Email);

            if (user == null ||
                !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError(
                    "",
                    "Galat Email ya Password."
                );

                return View(model);
            }

            var token = GenerateToken(user);

            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,

                // Local HTTPS par true rakho
                Secure = true,

                SameSite = SameSiteMode.Strict,

                Expires = DateTime.UtcNow.AddHours(2)
            });

            return RedirectToAction("Index", "StudentDashboard");
        }


        // --- LOGOUT ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");

            return RedirectToAction("Login");
        }

        // --- GENERATE TOKEN METHOD ---
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
        new Claim(ClaimTypes.Role, user.Role)
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
