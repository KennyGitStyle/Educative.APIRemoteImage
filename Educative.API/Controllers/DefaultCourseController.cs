using Educative.Core;
using Educative.Infrastructure.Interface;
using Educative.API.Errors;
using Microsoft.AspNetCore.Mvc;
using Educative.Infrastructure.Helpers;
using System.Text.Json;
using Educative.API.Dto;
using AutoMapper;
using Educative.API.Helpers;
using LazyCache;

namespace Educative.API.Controllers
{

    public sealed class DefaultCourseController : DefaultController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly string courseKey = "courseKey";
        private readonly IAppCache _appCache;

        public DefaultCourseController(IUnitOfWork unitOfWork, IMapper mapper, IAppCache appCache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appCache = appCache;
        }

        //[CachedAttribute(900)]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Course>))]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllCourse([FromQuery] CourseParams courseParams)
        {
            var cached = await _appCache.GetOrAddAsync(courseKey,  () =>  _unitOfWork.CourseRepository.GetAllCourses(courseParams), DateTimeOffset.Now.AddMinutes(25));

            Response.Headers.Add("Pagination", JsonSerializer.Serialize(cached.MetaData));

            Response.Headers.Add("Access-Control-Expose-Headers", "Pagination");

            var result = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(cached);

            return Ok(result);
        }

        //[CachedAttribute(900)]
        [HttpGet("{id}", Name = nameof(GetCourseById))]
        [ProducesResponseType(200, Type = typeof(Course))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CourseDto>> GetCourseById(string id)
        {
           if(string.IsNullOrWhiteSpace(id) && id == null){
                return BadRequest(new HttpErrorException(400)); // 400 bad request made
            }
           
           var cached = await _appCache.GetOrAddAsync(courseKey,  () =>  _unitOfWork.CourseRepository.GetByIdAsync(id), DateTimeOffset.Now.AddMinutes(25));
           
           if(cached.CourseId != id){
                return NotFound(new HttpErrorException(400)); // 400 Bad request  
            }
            
            return Ok(_mapper.Map<Course, CourseDto>(cached));
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Course))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Course>> AddCourse(Course course)
        {
            if (course == null){
                return BadRequest(new HttpErrorException(400)); // 400 Bad request
            }

            if (!ModelState.IsValid){
                return BadRequest(ModelState); // 400 Bad request
            }
            
            var added = await _unitOfWork.CourseRepository.AddAsync(course);
            
            return CreatedAtRoute(// 201 Created
            routeName: nameof(GetCourseById),
            routeValues: new { id = course.CourseId.ToLower() },
            value: added);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CourseDto>>UpdateCourse(string id, [FromBody]CourseDto courseDto)
        {
            if (courseDto == null || string.IsNullOrWhiteSpace(id)){
                return BadRequest(new HttpErrorException(400)); // 400 Bad request
            }

            if (!ModelState.IsValid){
                return BadRequest(new HttpErrorException(400)); // 400 Bad request
            }

            var existing = await _unitOfWork.CourseRepository.GetByIdAsync(id);

            if (id != existing.CourseId){
                return NotFound(new HttpErrorException(404)); // 404 Not Found Resource 
            }

            _mapper.Map(courseDto, existing);

            await _unitOfWork.CourseRepository.UpdateAsync(existing);

            return new NoContentResult(); // 204 No content
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<bool>> DeleteCourse(string id)
        {
            var existing = await _unitOfWork.CourseRepository.GetByIdAsync(id);

            if (existing == null){
                return NotFound(new HttpErrorException(404)); // 404 Resource not found
            }

            await _unitOfWork.CourseRepository.DeleteAsync(id);

            _appCache.Remove(courseKey);

            return NoContent(); // 204 No content
        }

        
    }
}
