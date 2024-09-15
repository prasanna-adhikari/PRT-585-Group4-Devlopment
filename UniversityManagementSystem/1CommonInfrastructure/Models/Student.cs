using _1CommonInfrastructure.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CommonInfrastructure.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters.")]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        public DateTime DoB { get; set; }  // Date of Birth field

        [Required]
        public Gender Gender { get; set; }  // Enum for Gender (Male, Female, Other)

        [Required]
        [StringLength(255, ErrorMessage = "Address cannot be longer than 255 characters.")]
        public string Address { get; set; }

        [Required]
        //[Phone(ErrorMessage = "Invalid phone number format.")]
        //[StringLength(10, ErrorMessage = "Phone number cannot be longer than 15 characters.")]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; }

        [Required]
        public int DepartmentId { get; set; }  // Foreign key

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }
    }
}
