using System.ComponentModel.DataAnnotations;

namespace ignou.Models
{
    public class Announcement
    {


        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Notice Title is required")]
        [StringLength(1000, ErrorMessage = "Title cannot exceed 1000 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Message content is required")]
        public string Message { get; set; }

       
        public DateTime PublishDate { get; set; } = DateTime.Now;

        
        public bool IsActive { get; set; } = true;

        
        [Required(ErrorMessage = "Please select a category")]
        public string Category { get; set; }

        
        public string? PostedBy { get; set; }


    }
}
