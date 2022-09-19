namespace Educative.API.Dto;

public class StudentCourseDto
{
    public string StudentId { get; set; } = string.Empty!;

    public virtual StudentDto StudentDto { get; set; }

    public string CourseId { get; set; } = string.Empty!;

    public virtual CourseDto CourseDto { get; set; }
}
