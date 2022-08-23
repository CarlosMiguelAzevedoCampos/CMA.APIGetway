using CMA.APIGetway.Books.API.Models;

namespace CMA.APIGetway.Books.API.Repository
{
    public class BookRepository
    {
        private IEnumerable<Book> _books { get; set; }
        public BookRepository()
        {
            _books = new[]
            {
            new Book
            {
                BookId = 1,
                AuthorId = 1,
                Name = "Harmony"
            },
            new Book
            {
                BookId = 2,
                AuthorId = 2,
                Name = "Time"
            }
        };
        }
        public IEnumerable<Models.Book> GetBooks() => _books;
    }
}
