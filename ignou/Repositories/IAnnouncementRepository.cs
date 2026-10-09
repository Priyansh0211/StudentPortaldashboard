using ignou.Models;

namespace ignou.Repositories
{
    public interface IAnnouncementRepository
    {
        Task<IEnumerable<Announcement>> GetAllAnnouncementAsync();
        Task AddAnnouncementAsync(Announcement announcement);

        Task EditAnnouncementAsync(Announcement announcement);

        Task DeleteAnnouncementAsync(int id);

        Task<Announcement> GetAnnouncementById(int id);
        Task SaveChangesAsync();
    }
}