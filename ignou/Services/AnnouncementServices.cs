using ignou.Models;
using ignou.Repositories;


namespace ignou.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _repository;

    
        public AnnouncementService(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Announcement>> GetAllAnnouncementAsync()
        {
            return await _repository.GetAllAnnouncementAsync();
        }

        public async Task AddAnnouncementAsync(Announcement announcement, string postedBy)
        {
            
            announcement.PublishDate = DateTime.Now;
            announcement.IsActive = true;
            announcement.PostedBy = postedBy;

            await _repository.AddAnnouncementAsync(announcement);
            await _repository.SaveChangesAsync();
        }

        public async Task EditAnnouncementAsync(Announcement announcement)
        {
            await _repository.EditAnnouncementAsync(announcement);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAnnouncementAsync(int id)
        {
            await _repository.DeleteAnnouncementAsync(id);
            await _repository.SaveChangesAsync();
        }

        public Task<Announcement> GetAnnouncementById(int id)
        {
            return _repository.GetAnnouncementById(id);
        }
    }
}