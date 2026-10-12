using ignou.Data;
using ignou.Models;
using Microsoft.EntityFrameworkCore;
using ignou.Models;

namespace ignou.Repositories
{
    public class LoginHistoryRepository : ILoginHistoryRepository
    {
        private readonly AppDbContext _db;

        public LoginHistoryRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddLoginRecordAsync(LoginHistory record)
        {
            await _db.LoginHistories.AddAsync(record);
        }

        public async Task<LoginHistory> GetLastLoginByEmailAsync(string email)
        {
            return await _db.LoginHistories
                            .Where(l => l.Email == email)
                            .OrderByDescending(l => l.LoginTime)
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<LoginHistory>> GetTodayLoginsByEmailAsync(string email)
        {
            return await _db.LoginHistories
                            .Where(l => l.Email == email && l.LoginTime.Date == DateTime.Today.Date)
                            .OrderByDescending(l => l.LoginTime)
                            .ToListAsync();
        }

        public async Task<IEnumerable<LoginHistory>> GetRecentLoginsAsync(int count, string role)
        {
            return await _db.LoginHistories
                            .Where(l => l.Role == role)
                            .OrderByDescending(l => l.LoginTime)
                            .Take(count)
                            .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}