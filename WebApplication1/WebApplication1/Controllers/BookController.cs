using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerAPI.Models;
using MessengerAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
/*
 * Controller for Book
 * It creates the book, gets the values, deletes and upadtes by calling
 * the methods defined in the book repository.
 * 
 * */

namespace MessengerAPI.Controllers
{
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET: api/values gets all the books
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            Console.WriteLine("HTTP Get");
            return _bookRepository.GetAll();
        }
        // gets book by id
        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult Get(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }


        // POST book adds book in the table
        [HttpPost]
        public IActionResult Post([FromBody]Book value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            var createdBook = _bookRepository.Add(value);

            
            return CreatedAtRoute("GetBook", new { id = createdBook.bookid }, createdBook);
        }


        // PUT book/5 updates book in the table
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Book value)
        {
            if (value == null)
            {
                return BadRequest();
            }


            var note = _bookRepository.GetById(id);


            if (note == null)
            {
                return NotFound();
            }


            value.bookid = id;
            _bookRepository.Update(value);


            return NoContent();


        }


        // DELETE book/5 deletes a book from the table
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            _bookRepository.Delete(book);


            return NoContent();
        }

    }
}
