using CMA.APIGetway.Authors.API.Models;

namespace CMA.APIGetway.Authors.API.Repository
{
    public class AuthorRepository
    {
        private IEnumerable<Author> _authors { get; set; }
        private bool _shouldFail = true;
        private DateTime _startTime = DateTime.UtcNow;
        public AuthorRepository()
        {
            _authors = new[]
            {
            new Author
            {
                AuthorId = 1,
                Name = "John Doe"
            },
            new Author
            {
                AuthorId = 2,
                Name = "Jane Smith"
            }
        };
        }
        public IEnumerable<Author> GetAuthors()
        {
            if (_shouldFail)
            {
                _shouldFail = false;
                throw new Exception("Oops!");
            }
            if (_startTime.AddMinutes(1) > DateTime.UtcNow)
            {
                Thread.Sleep(5000);
                throw new Exception("Timeout!");
            }
            return _authors;
        }
    }
}
