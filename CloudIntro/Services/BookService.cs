using CloudIntro.Repositories;

namespace CloudIntro.Services
{
    public interface IBookService
    {
        public IEnumerable<Book> FindBooks();
    }
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public IEnumerable<Book> FindBooks()
        {
            return _bookRepository.FindBooks();
        }
    }
}
