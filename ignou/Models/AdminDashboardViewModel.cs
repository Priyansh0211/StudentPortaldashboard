namespace ignou.Models
{
    public class AdminDashboardViewModel
    {
        
        public int TotalStudents { get; set; }
        public int ActiveStudents { get; set; }
        public int InactiveStudents { get; set; }

        // Subject/Program-wise count (Jaise: BCA = 50, MCA = 30)
        public Dictionary<string, int> ProgramWiseStudents { get; set; }

        
        public int TotalAnnouncements { get; set; }
        public int ActiveAnnouncements { get; set; }
        public int InactiveAnnouncements { get; set; }

        
        public IEnumerable<User> RecentStudents { get; set; }
        public IEnumerable<Announcement> RecentAnnouncements { get; set; }

        public IEnumerable<LoginHistory> RecentLogins { get; set; }
    }
}
