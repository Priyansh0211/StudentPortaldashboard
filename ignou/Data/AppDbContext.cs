using ignou.Models;
using Microsoft.EntityFrameworkCore;

namespace ignou.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<User> Users { get; set; } 
        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<ChatbotCategory> ChatbotCategories { get; set; }

        public DbSet<ChatbotFaq> ChatbotFAQs { get; set; }

        public DbSet<ChatbotOption> ChatbotOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Category -> FAQs
            modelBuilder.Entity<ChatbotFaq>()
                .HasOne(f => f.Category)
                .WithMany(c => c.FAQs)
                .HasForeignKey(f => f.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // FAQ -> Follow-up options
            modelBuilder.Entity<ChatbotOption>()
                .HasOne(o => o.Faq)
                .WithMany(f => f.FollowUpOptions)
                .HasForeignKey(o => o.FaqId)
                .OnDelete(DeleteBehavior.Cascade);

            // Follow-up option -> Next FAQ
            modelBuilder.Entity<ChatbotOption>()
                .HasOne(o => o.NextFaq)
                .WithMany()
                .HasForeignKey(o => o.NextFaqId)
                .OnDelete(DeleteBehavior.Restrict);

            // Category names should be unique
            modelBuilder.Entity<ChatbotCategory>()
                .HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}
