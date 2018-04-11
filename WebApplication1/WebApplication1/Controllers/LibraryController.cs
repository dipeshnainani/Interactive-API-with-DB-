using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MessengerAPI.Services;
using MessengerAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

/*
* Controller for Library
* It creates the library, gets the values, deletes and upadtes by calling
* the methods defined in the library repository.
* 
* */

namespace MessengerAPI.Controllers
{
    [Route("api/[controller]")]
    public class LibraryController : Controller
    {
        private readonly ILibraryRepository _libraryRepository;

        public LibraryController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        // GET: api/values gets the values from the tables
        [HttpGet]
        public IEnumerable<Library> Get()
        {
            Console.WriteLine("HTTP Get");
            return _libraryRepository.GetAll();
        }

        // gets the values by id from the tables
        [HttpGet("{id}", Name = "GetLibrary")]
        public IActionResult Get(int id)
        {
            var book = _libraryRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }


        // POST add a library to the table
        [HttpPost]
        public IActionResult Post([FromBody]Library value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            var createdLibrary = _libraryRepository.Add(value);


            return CreatedAtRoute("GetLibrary", new { id = createdLibrary.bookid }, createdLibrary);
        }


        // PUT updates values in the table
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Library value)
        {
            if (value == null)
            {
                return BadRequest();
            }


            var note = _libraryRepository.GetById(id);


            if (note == null)
            {
                return NotFound();
            }


            value.bookid = id;
            _libraryRepository.Update(value);


            return NoContent();


        }


        // DELETE book/5 deletes value from the table
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var library = _libraryRepository.GetById(id);
            if (library == null)
            {
                return NotFound();
            }
            _libraryRepository.Delete(library);


            return NoContent();
        }
    }
}

