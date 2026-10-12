using System.ComponentModel.DataAnnotations;

namespace ignou.Models
{
    public class ChatbotFaq
    {
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(500)]
        public string Question { get; set; } = string.Empty;

        [Required]
        [StringLength(3000)]
        public string Answer { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; } = 0;

        // FAQ belongs to a category
        public ChatbotCategory? Category { get; set; }

        // Follow-up options shown after this answer
        public ICollection<ChatbotOption> FollowUpOptions { get; set; }
            = new List<ChatbotOption>();
    }
}
