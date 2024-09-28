using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
namespace _1CommonInfrastructure.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }  // Primary Key

        [Required]
        [StringLength(200, ErrorMessage = "Course name cannot exceed 200 characters.")]
        public string CourseName { get; set; }  // Name of the course

        [Required]
        [StringLength(10, ErrorMessage = "Course code cannot exceed 10 characters.")]
        public string CourseCode { get; set; }  // Unique course code

        [Required]
        public int Credits { get; set; }  // Number of credits

        [ForeignKey("Department")]
        [Required]  // Ensure DepartmentId is required
        public int DepartmentId { get; set; }  // Foreign key to the Department table

        [Required]
        [StringLength(150, ErrorMessage = "Faculty name cannot exceed 150 characters.")]
        public string FacultyName { get; set; }  // Name of the faculty member

        [Required]
        [StringLength(10, ErrorMessage = "Semester cannot exceed 10 characters.")]
        public string SemesterOffered { get; set; }  // Semester in which the course is offered

        
    }
}
