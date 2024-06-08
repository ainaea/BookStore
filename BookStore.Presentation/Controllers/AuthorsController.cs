using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public AuthorsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return Ok(unitOfWork.Authors.GetAll()?.Select( a => MapToAuthorDTO(a) ));
        }
        [HttpPost]
        [Route("[Action]")]
        public IActionResult AddAuthor([FromBody] AuthorDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(unitOfWork.Authors.Add(MapToAuthor(dto, null)));
        }
        [HttpPost]
        [Route("[Action]")]
        public IActionResult EditAuthor([FromBody] AuthorDTO dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var author = unitOfWork.Authors.Get(dto.Id);
            if (author == null)
                return NotFound($"Author with Id {dto.Id} not found");
            unitOfWork.Authors.Update(MapToAuthor(dto, author));
            return Ok();
        }
        [HttpGet]
        [Route("[Action]/{id}")]
        public IActionResult GetAuthor([FromQuery] Guid id)
        {
            var author = unitOfWork.Authors.Get(id);
            if (author == null)
                return NotFound();
            return Ok(MapToAuthorDTO(author));
        }
        [HttpPost]
        [Route("[Action]")]
        public IActionResult DeleteAuthor([FromBody] AuthorDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var author = unitOfWork.Authors.Get(dto.Id);
            if (author == null)
                return NotFound($"Author with Id {dto.Id} not found");
            unitOfWork.Authors.Remove(MapToAuthor(dto, author));
            return Ok();
        }
        public static Author MapToAuthor(AuthorDTO dto, Author? model)
        {
            return new Author(dto.Name)
            {
                Id = model == null? new Guid() : model.Id,
            };
        }
        public static AuthorDTO MapToAuthorDTO(Author model)
        {
            return new AuthorDTO
            {
                Id = model.Id,
                Name = model.Name,
                Publications = model.Publications?.Select(x => BookController.MapToBookDTO(x))
            };
        }
    }
}
