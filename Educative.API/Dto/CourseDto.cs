using System.ComponentModel.DataAnnotations;

namespace Educative.API.Dto;
public class CourseDto
{



    public string CourseId { get; set; } = string.Empty!;

    [Required]
    public string CourseName { get; set; } = string.Empty!;

    [Required]
    public string CourseTutor { get; set; } = string.Empty!;

    [Required]

    public string CourseDescription { get; set; } = string.Empty!;

    [Required]
    public string CourseTopic { get; set; } = string.Empty!;
    public decimal Price { get; set; }
    public virtual ICollection<StudentCourseDto> StudentCourseDtos { get; set; }
}