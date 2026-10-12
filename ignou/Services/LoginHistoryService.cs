using ignou.Models;
using ignou.Repositories;

namespace ignou.Services
{
    public class LoginHistoryService : ILoginHistoryService
    {
        private readonly ILoginHistoryRepository _repository;

        public LoginHistoryService(ILoginHistoryRepository repository)
        {
            _repository = repository;
        }

        public async Task RecordUserLoginAsync(string email, string fullName, string role)
        {
            var record = new LoginHistory
            {
                Email = email,
                Fullname = fullName,
                Role = role,
                LoginTime = DateTime.Now
            };

            await _repository.AddLoginRecordAsync(record);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<LoginHistory>> GetRecentStudentLoginsAsync()
        {
            // Sirf students ka recent data, count 10
            return await _repository.GetRecentLoginsAsync(10, "Student");
        }

        public async Task<(LoginHistory LastLogin, int TodayCount, IEnumerable<LoginHistory> TodayLogins)> GetStudentLoginStatsAsync(string email)
        {
            var lastLogin = await _repository.GetLastLoginByEmailAsync(email);
            var todayLogins = await _repository.GetTodayLoginsByEmailAsync(email);

            return (lastLogin, todayLogins.Count(), todayLogins);
        }
    }
}