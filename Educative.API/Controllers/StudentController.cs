using AutoMapper;
using Educative.API.Dto;
using Educative.API.Errors;
using Educative.API.Filter;
using Educative.Core;
using Educative.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Educative.API.Controllers
{
    public class StudentController : DefaultController
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _unitOfWork.
            StudentRepository.FilterAttendanceAsync(x => x.Attendance >= 80);

            return Ok(_mapper.Map<IEnumerable<Student>, IEnumerable<StudentDto>>(students));
        }

        [HttpGet("{id}", Name = nameof(GetStudentById))]
        [ProducesResponseType(200, Type = typeof (StudentDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<StudentDto>> GetStudentById(string id)
        {
            if (string.IsNullOrWhiteSpace(id) && id == null)
            {
                return BadRequest(new HttpErrorException(400)); // 400 bad request made
            }

            var student = await _unitOfWork.StudentRepository.GetByIdAsync(id);

            if (student.StudentId != id)
            {
                return NotFound(new HttpErrorException(400)); // 400 Bad request
            }


            return Ok(_mapper.Map<Student, StudentDto>(student));
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(StudentDto))]
        [HttpDbExceptionFilter]
        [ProducesResponseType(400)]
        public async Task<ActionResult<StudentDto>> AddStudent(StudentDto studentDto)
        {

            if (studentDto == null && !ModelState.IsValid)
            {
                return BadRequest(new HttpErrorException(400)); // 400 Bad request
            }

            var student = _mapper.Map<Student>(studentDto);

            await _unitOfWork.StudentRepository.AddAsync(student);

            var createdStudent = _mapper.Map<StudentDto>(student);

            return CreatedAtRoute(// 201 Created
            routeName: nameof(GetStudentById),
            routeValues: new { id = createdStudent.StudentId.ToLower() },
            value: createdStudent);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<StudentDto>>UpdateStudent(string id, [FromBody] StudentDto studentDto)
        {
            if (studentDto == null || string.IsNullOrWhiteSpace(id))
            {
                return BadRequest(new HttpErrorException(400)); // 400 Bad request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new HttpErrorException(400)); // 400 Bad request
            }

            var existing = await _unitOfWork.StudentRepository.GetByIdAsync(id);

            if (id != existing.StudentId){
                return NotFound(new HttpErrorException(404)); // 404 Not Found Resource 
            }

            _mapper.Map(studentDto, existing);

            await _unitOfWork.StudentRepository.UpdateAsync(existing);
            
            return new NoContentResult(); // 204 No content
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<bool>> DeleteStudent(string id)
        {
            var existing = await _unitOfWork.StudentRepository.GetByIdAsync(id);

            if (id != existing.StudentId)
            {
                return NotFound(new HttpErrorException(404)); // 404 Resource not found
            }

            if(string.IsNullOrWhiteSpace(id) && id == null){
                return BadRequest(new HttpErrorException(400)); // 400 Bad request
            }

            await _unitOfWork.StudentRepository.DeleteAsync(id);

            return new NoContentResult(); // 204 No content
        }
    }
}
