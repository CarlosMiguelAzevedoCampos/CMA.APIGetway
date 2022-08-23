using CMA.APIGetway.Authors.API.Models;
using CMA.APIGetway.Authors.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CMA.APIGetway.Authors.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorRepository _authorRepository;

        public AuthorController(AuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet(Name = "authors")]
        public IEnumerable<Author> Get()
        {
            return _authorRepository.GetAuthors();
        }
    }
}