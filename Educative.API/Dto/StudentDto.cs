using System.ComponentModel.DataAnnotations;

namespace Educative.API.Dto;

public class StudentDto
{
    [Required]
    public string StudentId { get; set; } = string.Empty!;
    [Required]
    public string Firstname { get; set; } = string.Empty!;
    public char? MiddlenameInitial { get; set; } 
    public string Lastname { get; set; } = string.Empty!;
    public DateTime? DateOfBirth { get; set; }
    public virtual AddressDto AddressDto { get; set; }
    public string PhoneNo { get; set; } = string.Empty!;
    public string Email { get; set; } = string.Empty!;
    public int Attendance { get; set; }
    public virtual ICollection<StudentCourseDto> StudentCourseDtos { get; set; }

}