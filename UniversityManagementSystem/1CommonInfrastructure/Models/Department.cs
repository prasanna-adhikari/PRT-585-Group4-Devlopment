using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CommonInfrastructure.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }  // Primary Key

        [Required]
        [StringLength(200, ErrorMessage = "Department name cannot exceed 200 characters.")]
        public string DepartmentName { get; set; }  // Name of the department

        [Required]
        [StringLength(10, ErrorMessage = "Department code cannot exceed 10 characters.")]
        public string DepartmentCode { get; set; }  // Unique department code

        [Required]
        [StringLength(150, ErrorMessage = "Faculty head's name cannot exceed 150 characters.")]
        public string FacultyHead { get; set; }  // Name of the head of the department

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(150, ErrorMessage = "Contact email cannot exceed 150 characters.")]
        public string ContactEmail { get; set; }  // Contact email

        [Required]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(15, ErrorMessage = "Contact phone number cannot exceed 15 characters.")]
        public string ContactPhone { get; set; }  // Contact phone number
    }
}
