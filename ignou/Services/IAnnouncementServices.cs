using ignou.Models;

namespace ignou.Services
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<Announcement>> GetAllAnnouncementAsync();
        
        // Add karte waqt humein PostedBy ka naam backend se bhejna hai
        Task AddAnnouncementAsync(Announcement announcement, string postedBy);
        
        Task EditAnnouncementAsync(Announcement announcement);
        
        Task DeleteAnnouncementAsync(int id);

        Task<Announcement> GetAnnouncementById(int id);

        
    }
}