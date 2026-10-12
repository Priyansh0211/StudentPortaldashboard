using System.ComponentModel.DataAnnotations;

namespace ignou.Models
{
    public class LoginHistory
    {

        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Fullname  { get; set; }

        public string Role { get; set; }

        public DateTime LoginTime { get; set; }

    }
}
