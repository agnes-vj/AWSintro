using CloudIntro.Repositories;

namespace CloudIntro.Services
{
    public interface IBookService
    {
        public IEnumerable<Book> FindBooks();
        public void AddBook(Book newBook);
        public Book GetBook(int id);
        public void RemoveBook(int id);
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
        public Book GetBook(int id)
        {
            return _bookRepository.GetBook(id);
        }
        public void AddBook(Book newBook)
        {
            _bookRepository.AddBook(newBook);
        }
        public void RemoveBook(int id)
        {
            _bookRepository.RemoveBook(id);            
        }
    }
}
