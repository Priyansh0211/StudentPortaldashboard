using ignou.Data;
using ignou.Models;
using Microsoft.EntityFrameworkCore;

namespace ignou.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly AppDbContext _db;

        public AnnouncementRepository(AppDbContext db)
        {
            _db = db;
        }

        // 1. GET ALL
        public async Task<IEnumerable<Announcement>> GetAllAnnouncementAsync()
        {
            return await _db.Announcements
                            .OrderByDescending(a => a.PublishDate)
                            .ToListAsync();
        }

        // 2. ADD
        public async Task AddAnnouncementAsync(Announcement announcement)
        {
            await _db.Announcements.AddAsync(announcement);
        }

        // 3. EDIT / UPDATE
        public Task EditAnnouncementAsync(Announcement announcement)
        {
            _db.Announcements.Update(announcement);
            return Task.CompletedTask; // Update synchronous method hai, isliye Task.CompletedTask return karte hain
        }

        // 4. DELETE
        public async Task DeleteAnnouncementAsync(int id)
        {
            // Pehle database mein check karo ki is ID ka notice hai ya nahi
            var announcement = await _db.Announcements.FindAsync(id);
            if (announcement != null)
            {
                _db.Announcements.Remove(announcement);
            }
        }

        public async Task<Announcement> GetAnnouncementById(int id)
        {
            return await _db.Announcements.FirstOrDefaultAsync(a => a.Id == id);
            
        }

        // 5. SAVE CHANGES
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}