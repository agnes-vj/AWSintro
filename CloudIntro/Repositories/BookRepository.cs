namespace CloudIntro.Repositories
{
    public interface IBookRepository
    {
        public IEnumerable<Book> FindBooks();
        public void AddBook(Book newBook);
        public void RemoveBook(int id);
        public Book GetBook(int id);
    }
    public class BookRepository : IBookRepository
    {
        private readonly BookShopContext _bookShopContext;
        private int lastBookId = 0;
        public BookRepository(BookShopContext bookShopContext) 
        {
            _bookShopContext = bookShopContext;
        }
        public IEnumerable<Book> FindBooks()
        {
            return _bookShopContext.Books;
        }
        public Book GetBook(int id)
        {
            Book book = _bookShopContext.Books.Find(id);
            if (book == null)
                throw new Exception($"Book with id: {id} doesn't exist");
            return book;            
        }

        public void AddBook(Book newBook)
        {
            try
            {
                _bookShopContext.Books.Add(newBook);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
            _bookShopContext.SaveChanges();
        }
        public void RemoveBook(int id)
        {
            Book? bookToRemove = _bookShopContext.Books.Find(id);
            if (bookToRemove == null)
                throw new Exception($"Book with id : {id} not found");
            _bookShopContext.Books.Remove(bookToRemove);
            _bookShopContext.SaveChanges();
        }

    }
}
