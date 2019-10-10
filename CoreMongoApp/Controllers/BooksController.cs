using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMongoApp.Models;
using CoreMongoApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreMongoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            var books = _bookService.Get();
            return books;
        }

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var book = _bookService.Get(id);

            if(book == null)
            {
                return NotFound();
            }

            return book;
        }


        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            _bookService.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Book book)
        {
            var existing = _bookService.Get(id);

            if(existing == null)
            {
                return NotFound();
            }

            _bookService.Update(id, book);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult Delete(string id)
        {
            var book = _bookService.Get(id);

            if(book == null)
            {
                return NotFound();
            }

            _bookService.Remove(id);

            return NoContent();
        }
    }
}