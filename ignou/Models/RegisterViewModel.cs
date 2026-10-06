using System.ComponentModel.DataAnnotations;

namespace ignou.Models
{
    public class RegisterViewModel
    {
        // --- Personal Details ---
        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, RegularExpression(@"^\d{10}$", ErrorMessage = "10 digit mobile number enter karein.")]
        public string MobileNo { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string FatherName { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string BloodGroup { get; set; }

        [Required, RegularExpression(@"^\d{12}$", ErrorMessage = "12 digit Aadhaar number enter karein.")]
        public string AadhaarNo { get; set; }

        [Required]
        public string MaritalStatus { get; set; }

        [Required]
        public string PersonalDisability { get; set; } // Yes/No

        // --- Address Details ---
        [Required]
        public string House { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public string State { get; set; }

        [Required, RegularExpression(@"^\d{6}$", ErrorMessage = "Valid 6 digit Pincode enter karein.")]
        public string Pincode { get; set; }

        // --- Program Details ---
        [Required]
        public string ProgramType { get; set; }

        [Required]
        public string ProgramName { get; set; }
    }
}
