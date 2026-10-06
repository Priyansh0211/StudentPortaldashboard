using System.ComponentModel.DataAnnotations;

namespace ignou.Models
{
    public class RegisterViewModel
    {
        // =========================================================
        // PERSONAL DETAILS
        // =========================================================

        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(
            100,
            MinimumLength = 2,
            ErrorMessage = "Full name must be between 2 and 100 characters."
        )]
        public string FullName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Father's name is required.")]
        [StringLength(
            100,
            MinimumLength = 2,
            ErrorMessage = "Father's name must be between 2 and 100 characters."
        )]
        public string FatherName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }


        [Required(ErrorMessage = "Please select your gender.")]
        public string Gender { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please select your category.")]
        public string Category { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please select your blood group.")]
        public string BloodGroup { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please select your marital status.")]
        public string MaritalStatus { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please select your disability status.")]
        public string PersonalDisability { get; set; } = string.Empty;


        [Required(ErrorMessage = "Aadhaar number is required.")]
        [RegularExpression(
            @"^\d{12}$",
            ErrorMessage = "Aadhaar number must contain exactly 12 digits."
        )]
        public string AadhaarNo { get; set; } = string.Empty;


        // =========================================================
        // CONTACT & LOGIN DETAILS
        // =========================================================

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(
            ErrorMessage = "Please enter a valid email address."
        )]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(
            @"^[6-9]\d{9}$",
            ErrorMessage = "Please enter a valid 10-digit mobile number."
        )]
        public string MobileNo { get; set; } = string.Empty;


        [Required(ErrorMessage = "Password is required.")]
        [StringLength(
            100,
            MinimumLength = 8,
            ErrorMessage = "Password must be at least 8 characters long."
        )]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;


        // =========================================================
        // ADDRESS DETAILS
        // =========================================================

        [Required(ErrorMessage = "House / street address is required.")]
        [StringLength(
            250,
            ErrorMessage = "Address cannot exceed 250 characters."
        )]
        public string House { get; set; } = string.Empty;


        [Required(ErrorMessage = "City is required.")]
        [StringLength(
            100,
            ErrorMessage = "City name cannot exceed 100 characters."
        )]
        public string City { get; set; } = string.Empty;


        [Required(ErrorMessage = "District is required.")]
        [StringLength(
            100,
            ErrorMessage = "District name cannot exceed 100 characters."
        )]
        public string District { get; set; } = string.Empty;


        [Required(ErrorMessage = "State is required.")]
        [StringLength(
            100,
            ErrorMessage = "State name cannot exceed 100 characters."
        )]
        public string State { get; set; } = string.Empty;


        [Required(ErrorMessage = "Pincode is required.")]
        [RegularExpression(
            @"^\d{6}$",
            ErrorMessage = "Pincode must contain exactly 6 digits."
        )]
        public string Pincode { get; set; } = string.Empty;


        // =========================================================
        // PROGRAMME DETAILS
        // =========================================================

        [Required(ErrorMessage = "Please select a programme type.")]
        public string ProgramType { get; set; } = string.Empty;


        [Required(ErrorMessage = "Please select a programme.")]
        public string ProgramName { get; set; } = string.Empty;
    }
}