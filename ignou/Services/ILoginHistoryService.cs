using ignou.Models;

namespace ignou.Services
{
    public interface ILoginHistoryService
    {
        // Login save karne ke liye
        Task RecordUserLoginAsync(string email, string fullName, string role);

        // Dashboard pe top 10 dikhane ke liye
        Task<IEnumerable<LoginHistory>> GetRecentStudentLoginsAsync();

        // Kisi specific student ka record nikalne ke liye
        Task<(LoginHistory LastLogin, int TodayCount, IEnumerable<LoginHistory> TodayLogins)> GetStudentLoginStatsAsync(string email);
    }
}