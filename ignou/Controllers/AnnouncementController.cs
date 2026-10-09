using ignou.Data;
using ignou.Models;
using ignou.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ignou.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AnnouncementController : Controller
    {

        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
           _announcementService = announcementService;
        }

        [HttpGet]
        public async Task<IActionResult> ManageAnnouncements()
        {
            var notices = await _announcementService.GetAllAnnouncementAsync();
            return View(notices);
        }

        [HttpGet]
        public IActionResult CreateAnnouncement()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAnnouncement(Announcement model)
        {
            if (ModelState.IsValid)
            {
                
                await _announcementService.AddAnnouncementAsync(model, "Admin");
                return RedirectToAction("ManageAnnouncements");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AnnouncementDetails(int id)
        {
            var announcement = await _announcementService.GetAnnouncementById(id);
            return View(announcement);
        }

        [HttpGet]
        public async Task<IActionResult> EditAnnouncement(int id)
        {
            var announcement = await _announcementService.GetAnnouncementById(id);
            return View(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> EditAnnouncement(int id , Announcement announcement)
        {

            if (id != announcement.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(announcement);
            }

            await _announcementService.EditAnnouncementAsync(announcement);


            return RedirectToAction("ManageAnnouncements","Announcement");
        }

    }
}
