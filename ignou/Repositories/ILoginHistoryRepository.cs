using ignou.Models;

namespace ignou.Repositories
{
    public interface ILoginHistoryRepository
    {
        Task AddLoginRecordAsync(LoginHistory record);
        Task<LoginHistory> GetLastLoginByEmailAsync(string email);
        Task<IEnumerable<LoginHistory>> GetTodayLoginsByEmailAsync(string email);
        Task<IEnumerable<LoginHistory>> GetRecentLoginsAsync(int count, string role);
        Task SaveChangesAsync();

    }
}
