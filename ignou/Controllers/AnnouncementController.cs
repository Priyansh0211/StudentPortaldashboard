using ignou.Data;
using Microsoft.AspNetCore.Mvc;

namespace ignou.Controllers
{
    public class AnnouncementController : Controller
    {

        private readonly AppDbContext _db;

        public AnnouncementController(AppDbContext db)
        {
            _db = db;
        }

    }
}
