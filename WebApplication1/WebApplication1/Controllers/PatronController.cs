using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerAPI.Models;
using MessengerAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
/*
 * Controller for Patron
 * It creates the patron, gets the values, deletes and upadtes by calling
 * the methods defined in the patron repository.
 * 
 * */

namespace MessengerAPI.Controllers
{
    [Route("api/[controller]")]
    public class PatronController : Controller
    {
        private readonly IPatronRepository _patronRepository;

        public PatronController(IPatronRepository patronRepository)
        {
            _patronRepository = patronRepository;
        }

        // GET: api/values gets all the patrons
        [HttpGet]
        public IEnumerable<Patron> Get()
        {
            Console.WriteLine("HTTP Get");
            return _patronRepository.GetAll();
        }
        [HttpGet("{id}", Name = "GetPatron")]
        public IActionResult Get(int id)
        {
            var book = _patronRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }


        // POST book adds the patrons 
        [HttpPost]
        public IActionResult Post([FromBody]Patron value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            var createdBook = _patronRepository.Add(value);


            return CreatedAtRoute("GetBook", new { id = createdBook.patronid }, createdBook);
        }


        // PUT book/5 updates the values in the table
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Patron value)
        {
            if (value == null)
            {
                return BadRequest();
            }


            var note = _patronRepository.GetById(id);


            if (note == null)
            {
                return NotFound();
            }


            value.patronid = id;
            _patronRepository.Update(value);


            return NoContent();


        }


        // DELETE book/5 deletes the values from the tables
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _patronRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            _patronRepository.Delete(book);


            return NoContent();
        }

    }
}
