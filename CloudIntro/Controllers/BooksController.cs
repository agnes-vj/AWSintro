using CloudIntro.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace CloudIntro.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [EnableRateLimiting("fixed")]
        [HttpGet("/books")]
        public IActionResult Index()
        {
            return Ok(_bookService.FindBooks());
        }
    }
}
