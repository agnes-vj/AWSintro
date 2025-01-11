using CloudIntro.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Memory;

namespace CloudIntro.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;

        }

        //[EnableRateLimiting("fixed")]
        [HttpGet("/books")]
        public IActionResult GetAllBooks()
        { 
            return Ok(_bookService.FindBooks());
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            Book book;
            try
            {
                book = _bookService.GetBook(id);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(book);
        }

        [HttpPost]
        public IActionResult AddNewBook([FromBody] Book newBook)
        {
            try
            {
                _bookService.AddBook(newBook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok(newBook);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveBookById(int id)
        {
            try
            {
                _bookService.RemoveBook(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok($"Book with id: {id} removed successfully");
        }
    }
}
