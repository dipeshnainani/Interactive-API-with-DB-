using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MessengerAPI.Services;
using MessengerAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
/*
 * Controller for Author
 * It creates the author, gets the values, deletes and upadtes by calling
 * the methods defined in the author repository.
 * 
 * */
namespace MessengerAPI.Controllers
{
    [Route("[controller]")]
    public class AuthorController : Controller
    {
        private readonly IAuthorRespository _authorsRepository;

        public AuthorController(IAuthorRespository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        // GET: api/values gets all the authors in the tables
        [HttpGet]
        public IEnumerable<Author> Get()
        {
            Console.WriteLine("HTTP Get");
            return _authorsRepository.GetAll();
        }

        // gets one single author
        [HttpGet("{id}", Name = "GetAuthor")]
        public IActionResult Get(int id)
        {
            var book = _authorsRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }


        // POST book adds an author in the tables
        [HttpPost]
        public IActionResult Post([FromBody]Author value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            var createdAuthor = _authorsRepository.Add(value);


            return CreatedAtRoute("GetAuthor", new { id = createdAuthor.authorid }, createdAuthor);
        }


        // PUT book/5 updates the values in the table
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Author value)
        {
            if (value == null)
            {
                return BadRequest();
            }


            var note = _authorsRepository.GetById(id);


            if (note == null)
            {
                return NotFound();
            }


            value.authorid = id;
            _authorsRepository.Update(value);


            return NoContent();


        }


        // DELETE book/5 deletes the values from the table  
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var author = _authorsRepository.GetById(id);
            if (author == null)
            {
                return NotFound();
            }
            _authorsRepository.Delete(author);


            return NoContent();
        }
    }
}
