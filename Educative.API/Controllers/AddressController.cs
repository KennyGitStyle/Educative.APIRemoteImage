using AutoMapper;
using Educative.Core;
using Educative.API.Dto;
using Educative.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Educative.API.Controllers
{
    public class AddressController : DefaultController
    {
        private readonly IAddressRepository _addressRepo;
        private readonly IMapper _mapper;

        public AddressController(IAddressRepository addressRepo, IMapper mapper)
        {
            _addressRepo = addressRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Address>))]
        public async Task<IActionResult> GetAddressAll()
        {
            var address = await _addressRepo.GetAllAsync();
            var addressesToReturn = _mapper.Map<IEnumerable<AddressDto>>(address);
            return Ok(addressesToReturn);
        }
        

        [HttpGet("{id}", Name = nameof(GetAddressById))]
        [ProducesResponseType(200, Type = typeof (Address))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAddressById(string id)
        {
            var student = await _addressRepo.GetByIdAsync(id);
            var studentToReturn = _mapper.Map<AddressDto>(student);
            return Ok(studentToReturn);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Address))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddAddress(AddressDto addressDto)
        {
            if (addressDto == null)
            {
                return BadRequest(); // 400 Bad request
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad request
            }

            var addressToCreateDto = _mapper.Map<Address>(addressDto);
            var added = await _addressRepo.AddAsync(addressToCreateDto);
             
            
            return CreatedAtRoute(// 201 Created
            routeName: nameof(GetAddressById),
            routeValues: new { id = added.AddressId.ToLower() },
            value: added);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult>UpdateStudent(string id, [FromBody] Address address)
        {
            if (address == null || address.AddressId != id)
            {
                return BadRequest(); // 400 Bad request
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad request
            }

            var existing = await _addressRepo.GetByIdAsync(id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }
            await _addressRepo.UpdateAsync(address);
            return new NoContentResult(); // 204 No content
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<bool>> DeleteStudent(string id)
        {
            var existing = await _addressRepo.GetByIdAsync(id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }
            await _addressRepo.DeleteAsync(id);
            return new NoContentResult(); // 204 No content
        }
    }
}
