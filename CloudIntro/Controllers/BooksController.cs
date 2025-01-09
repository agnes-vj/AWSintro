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
        private readonly IMemoryCache _catch;

        public BooksController(IBookService bookService, IMemoryCache memoryCatch)
        {
            _bookService = bookService;
            _catch = memoryCatch;
        }

        [EnableRateLimiting("fixed")]
        [HttpGet("/books")]
        public IActionResult Index()
        {
            string booksCatchKey = "BookList";
            List<Book> books;

            if (!_catch.TryGetValue(booksCatchKey, out books))
            {
                books = _bookService.FindBooks().ToList();

                var catchEntryOptions = new MemoryCacheEntryOptions()
                                                                .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
                _catch.Set(booksCatchKey, books, catchEntryOptions);
            }

            return Ok(books);
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
