using CMA.APIGetway.Books.API.Models;
using CMA.APIGetway.Books.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CMA.APIGetway.Books.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookRepository _bookRepository;

        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet(Name = "books")]
        public IEnumerable<Book> Get()
        {
            return _bookRepository.GetBooks();
        }
    }
}