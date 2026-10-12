using System.ComponentModel.DataAnnotations;

namespace ignou.Models
{
    public class ChatbotOption
    {
        public int Id { get; set; }

        [Required]
        public int FaqId { get; set; }

        [Required]
        [StringLength(300)]
        public string OptionText { get; set; } = string.Empty;

        // The FAQ to open when the user selects this option
        public int? NextFaqId { get; set; }

        public int DisplayOrder { get; set; } = 0;

        // FAQ whose answer displays these options
        public ChatbotFaq? Faq { get; set; }

        // Next FAQ selected by the user
        public ChatbotFaq? NextFaq { get; set; }
    }
}