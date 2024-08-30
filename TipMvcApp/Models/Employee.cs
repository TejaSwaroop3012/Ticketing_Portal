using System.ComponentModel.DataAnnotations;

namespace TipMvcApp.Models
{
    public class Employee
    {
        [Display(Name = "Employee Id")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter the Employee Id greater than 1")]
        public int EmpId { get; set; }

        [MaxLength(30, ErrorMessage ="First Name cannot be more than 30 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [MaxLength(30, ErrorMessage = "Last Name cannot be more than 30 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^\s]+$",ErrorMessage ="Incorrect Email Id Format")]
        [Display(Name = "Email Id")]
        public string? EmailId { get; set; }

        [RegularExpression(@"\d{10}",ErrorMessage ="Phone Number must be 10 Digits")]
        [Display(Name = "Mobile Number")]
        public string MobileNo { get; set; } = null!;
    }
}
