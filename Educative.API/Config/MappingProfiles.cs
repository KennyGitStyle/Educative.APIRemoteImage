using AutoMapper;
using Educative.Core;
using Educative.API.Dto;

namespace Educative.API.Config;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<Student, StudentDto>().ReverseMap();
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<StudentCourse, StudentCourseDto>();
    }
}