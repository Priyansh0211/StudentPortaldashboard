using ignou.Data;
using ignou.Models;
using Microsoft.EntityFrameworkCore;

namespace ignou.Services
{
    public class ChatbotService
    {
        private readonly AppDbContext _db;

        public ChatbotService(AppDbContext db)
        {
            _db = db;
        }

        // Get all active chatbot categories
        public async Task<List<ChatbotCategory>> GetCategoriesAsync()
        {
            return await _db.ChatbotCategories
                .AsNoTracking()
                .Where(c => c.IsActive)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        // Get active FAQs for a selected category
        public async Task<List<ChatbotFaq>> GetFAQsByCategoryAsync(
            int categoryId)
        {
            return await _db.ChatbotFAQs
                .AsNoTracking()
                .Where(f => f.CategoryId == categoryId && f.IsActive)
                .OrderBy(f => f.DisplayOrder)
                .ThenBy(f => f.Question)
                .ToListAsync();
        }

        // Get one FAQ with its follow-up options
        public async Task<ChatbotFaq?> GetFAQByIdAsync(int faqId)
        {
            return await _db.ChatbotFAQs
                .AsNoTracking()
                .Where(f => f.Id == faqId && f.IsActive)
                .Include(f => f.FollowUpOptions
                    .OrderBy(o => o.DisplayOrder))
                .FirstOrDefaultAsync();
        }
    }
}
