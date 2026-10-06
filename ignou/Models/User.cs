using System.ComponentModel.DataAnnotations;

namespace ignou.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string MobileNo { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string FatherName { get; set; } = string.Empty;

        public DateTime DOB { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string BloodGroup { get; set; } = string.Empty;

        public string AadhaarNo { get; set; } = string.Empty;

        public string MaritalStatus { get; set; } = string.Empty;

        public string PersonalDisability { get; set; } = string.Empty;

        public string House { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string District { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string Pincode { get; set; } = string.Empty;

        public string ProgramType { get; set; } = string.Empty;

        public string ProgramName { get; set; } = string.Empty;

        public string? ApplicationNumber { get; set; }

        public string? EnrollmentNumber { get; set; }

        public string Role { get; set; } = "Student";
    }
}