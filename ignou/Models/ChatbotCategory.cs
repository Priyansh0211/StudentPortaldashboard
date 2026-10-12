using System.ComponentModel.DataAnnotations;

namespace ignou.Models
{
    public class ChatbotCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<ChatbotFaq> FAQs { get; set; }
            = new List<ChatbotFaq>();
    }
}
