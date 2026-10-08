using System.ComponentModel.DataAnnotations;

namespace ignou.Models
{
    public class AdminRegisterViewModel
    {

        
        [Required] 
        public string FullName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }

        public string? EmployeeId { get; set; }
        [Required]
        public string Department { get; set; }
        public string Designation { get; set; }

        
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string FatherName { get; set; }
        public DateTime DOB { get; set; }
        [Required]
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public string MaritalStatus { get; set; }
        [Required]
        public string AadhaarNo { get; set; }

        
        [Required]
        public string House { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Pincode { get; set; }

    }
}
