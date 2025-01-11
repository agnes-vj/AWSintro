using CloudIntro.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;

namespace CloudIntro.Services
{
    public interface IBookService
    {
        public List<Book> FindBooks();
        public void AddBook(Book newBook);
        public Book GetBook(int id);
        public void RemoveBook(int id);
    }
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMemoryCache _catch;
        public BookService(IBookRepository bookRepository, IMemoryCache memoryCatch)
        {
            _bookRepository = bookRepository;
            _catch = memoryCatch;
        }
        public List<Book> FindBooks()
        {
            string booksCatchKey = "BookList";
            List<Book> books;

            if (!_catch.TryGetValue(booksCatchKey, out books))
            {
                books = _bookRepository.FindBooks().ToList();

                var catchEntryOptions = new MemoryCacheEntryOptions()
                                                                .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
                _catch.Set(booksCatchKey, books, catchEntryOptions);
            }
            return books;
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
