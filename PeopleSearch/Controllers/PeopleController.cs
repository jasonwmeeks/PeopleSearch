using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleSearch.Models;
using PeopleSearch.Repositories;
using PeopleSearch.DTO;

namespace PeopleSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDataRepository<Person> _repo;

        public PeopleController(IMapper mapper, IDataRepository<Person> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            return await _repo.GetAll().ToListAsync();
        }

        // GET: api/<controller>/search/{fullName}
        [HttpGet("search/{term?}")]
        public async Task<ActionResult<IEnumerable<Person>>> SearchPersons(string term = "")
        {
            //Simulate search being slow and have the UI gracefully handle the delay
            var numSeconds = new Random().Next(5000);
            System.Threading.Thread.Sleep(numSeconds);

            var people = await _repo.GetAll().ToListAsync();
            if (string.IsNullOrEmpty(term))
                return people;
            
            return people.Where(x => x.FullName.Contains(term)).ToList();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _repo.GetById(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/People/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson([FromRoute] int id, [FromBody] EditPersonDto editPersonDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != editPersonDto.Id)
            {
                return BadRequest();
            }

            var prePerson = _mapper.Map<Person>(editPersonDto);
            _repo.Update(prePerson);
            await _repo.SaveAsync(prePerson);

            return NoContent();
        }

        // POST: api/People
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson([FromBody] AddPersonDto addPersonDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prePerson= _mapper.Map<Person>(addPersonDto);
            _repo.Add(prePerson);
            var savePerson = await _repo.SaveAsync(prePerson);
            var personResponse = _mapper.Map<PersonDto>(savePerson);

            return StatusCode(201, new { personResponse });
        }
    }
}
