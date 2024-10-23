using Microsoft.EntityFrameworkCore;

namespace CloudIntro
{
    public class BookShopContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public BookShopContext(DbContextOptions<BookShopContext> options)
        : base(options) { }
    }
}
