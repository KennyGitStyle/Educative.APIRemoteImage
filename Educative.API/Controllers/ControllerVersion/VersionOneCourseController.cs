using AutoMapper;
using Educative.API.Dto;
using Educative.API.Errors;
using Educative.Core;
using Educative.Infrastructure.Interface;
using LazyCache;
using Microsoft.AspNetCore.Mvc;

namespace Educative.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("/Courses")]
    public class VersionOneCourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private const string cacheKey = "courseKey";
        private readonly IMapper _mapper;
        private readonly IAppCache _appCache;

        public VersionOneCourseController(IUnitOfWork unitOfWork, IMapper mapper, IAppCache appCache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appCache = appCache;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof (IEnumerable<Course>))]
        public async Task<ActionResult<IEnumerable<Course>>> V1GetAllCourse()
        {
            var cached = await _appCache.GetOrAddAsync(cacheKey,  () =>  _unitOfWork.CourseRepository.GetCoursesExplicitly(), DateTimeOffset.Now.AddMinutes(25));

            var result = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(cached);

            return Ok(result);
        }
        [HttpGet("/CourseGereaterThanTen")]
        [ProducesResponseType(200, Type = typeof (Course))]
        public async Task<ActionResult<Course>> V1GetCourseEx()
        {
            var course = await _unitOfWork.CourseRepository.GetCourseExplicitly();

            return Ok(_mapper.Map<Course, CourseDto>(course));
        }

        [HttpGet("{id}", Name = nameof(V1GetCourseById))]
        [ProducesResponseType(200, Type = typeof (Course))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Course>> V1GetCourseById(string id)
        {
            var course = await _unitOfWork.CourseRepository.GetByIdAsync(id);

            if(course == null)
            {
                return NotFound(new HttpErrorException(404));
            }
            return Ok(course);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof (Course))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Course>> V1AddCourse(Course course)
        {
            if (course == null)
            {
                return BadRequest(new HttpErrorException(400)); // 400 Bad request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad request
            }

            var added = await _unitOfWork.CourseRepository.AddAsync(course);
            return CreatedAtRoute(// 201 Created
            routeName: nameof(V1GetCourseById),
            routeValues: new { id = course.CourseId.ToLower() },
            value: added);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Course>> V1UpdateCourse(string id, [FromBody] Course course)
        {
            if (course == null || course.CourseId != id)
                return BadRequest(new HttpErrorException(400)); // 400 Bad request

            if (!ModelState.IsValid)
            {
                return BadRequest(new HttpErrorException(400)); // 400 Bad request
            }

            var existing = await _unitOfWork.CourseRepository.GetByIdAsync(id);

            if (existing == null)
            {
                return NotFound(new HttpErrorException(404)); // 404 Resource not found
            }
            await _unitOfWork.CourseRepository.UpdateAsync(course);

            return new NoContentResult(); // 204 No content
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<bool>> V1DeleteCourse(string id)
        {
            var existing = await _unitOfWork.CourseRepository.GetByIdAsync(id);
            
            if (existing == null)
            {
                return NotFound(new HttpErrorException(404)); // 404 Resource not found
            }

            await _unitOfWork.CourseRepository.DeleteAsync(id);

            return new NoContentResult(); // 204 No content
        }

    }
}