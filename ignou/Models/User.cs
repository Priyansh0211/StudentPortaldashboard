using System.ComponentModel.DataAnnotations;

namespace ignou.Models
{
    public class User
    { 
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string MobileNo { get; set; }
        public string PasswordHash { get; set; }
        public string FatherName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Category { get; set; }
        public string BloodGroup { get; set; }
        public string AadhaarNo { get; set; }
        public string MaritalStatus { get; set; }
        public string PersonalDisability { get; set; }
        public string House { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string ProgramType { get; set; }
        public string ProgramName { get; set; }
        public string Role { get; set; } = "Student";
    }
}
