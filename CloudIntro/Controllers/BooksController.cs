using CloudIntro.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudIntro.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet("/books")]
        public IActionResult Index()
        {
            return Ok(_bookService.FindBooks());
        }
    }
}
