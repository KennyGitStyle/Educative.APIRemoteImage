namespace Educative.Core;

public class StudentCourse
{
    public string StudentId { get; set; } = string.Empty!;

    public virtual Student Student { get; set; }

    public string CourseId { get; set; } = string.Empty!;

    public virtual Course Course { get; set; }
}

