using System.ComponentModel.DataAnnotations;

namespace Educative.Core
{
    public class Course
    {
       

        [Key]
        public string CourseId { get; set; } = string.Empty!; 

        [Required]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; } = string.Empty!;

        [Required]
        [Display(Name = "Course Tutor")]
        public string CourseTutor { get; set; } = string.Empty!;

        [Required]
        [Display(Name = "Course Description")]
        //[StringLength(100, ErrorMessage = "Not enough Description!")]
        public string CourseDescription { get; set; } = string.Empty!;

        [Required]
        [StringLength(60)]
        public string CourseTopic { get; set; } = string.Empty!;
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; } 
    }
}
