using System.ComponentModel.DataAnnotations;

namespace ignou.Models
{
    public class EditStudentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, MinimumLength = 2)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Please enter a valid 10-digit mobile number.")]
        public string MobileNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Father's name is required.")]
        public string FatherName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Blood group is required.")]
        public string BloodGroup { get; set; } = string.Empty;

        [Required(ErrorMessage = "Aadhaar number is required.")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Aadhaar number must contain exactly 12 digits.")]
        public string AadhaarNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Marital status is required.")]
        public string MaritalStatus { get; set; } = string.Empty;

        [Required(ErrorMessage = "Disability status is required.")]
        public string PersonalDisability { get; set; } = string.Empty;

        [Required(ErrorMessage = "House / street address is required.")]
        public string House { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "District is required.")]
        public string District { get; set; } = string.Empty;

        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pincode is required.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Pincode must contain exactly 6 digits.")]
        public string Pincode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Program type is required.")]
        public string ProgramType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Program name is required.")]
        public string ProgramName { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}