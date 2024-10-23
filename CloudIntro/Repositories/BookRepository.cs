namespace CloudIntro.Repositories
{
    public interface IBookRepository
    {
        public IEnumerable<Book> FindBooks();
    }
    public class BookRepository : IBookRepository
    {
        private readonly BookShopContext _bookShopContext;
        public BookRepository(BookShopContext bookShopContext) 
        {
            _bookShopContext = bookShopContext;
        }
        public IEnumerable<Book> FindBooks()
        {
            return _bookShopContext.Books;
        }
    }
}
