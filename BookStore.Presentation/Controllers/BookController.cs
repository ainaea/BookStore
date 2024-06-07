using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Presentation.DTOs;
using BookStore.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private object[]? include = new object[] { typeof(Genre), typeof(Author) };
        public BookController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(unitOfWork.Books.GetAll(include)?.Select(a => MapToBookDTO(a)));
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] BookDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var checkResult = BasicChecks(dto);
            if (checkResult != null)
                return NotFound(checkResult);
            return Ok(unitOfWork.Books.Add(MapToBook(dto, null)));
        }
        [HttpPost]
        public IActionResult EditBook([FromBody] BookDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var book = unitOfWork.Books.Get(dto.Id);
            if (book == null)
                return NotFound($"Book with Id {dto.Id} not found");
            var checkResult = BasicChecks(dto);
            if (checkResult != null)
                return NotFound(checkResult);
            unitOfWork.Books.Update(MapToBook(dto, book));
            return Ok();
        }
        [HttpGet]
        public IActionResult GetBook([FromQuery] Guid id)
        {
            var book = unitOfWork.Books.Get(id);
            if (book == null)
                return NotFound();
            return Ok(MapToBookDTO(book));
        }
        [HttpGet]
        public IActionResult GetBooksByAuthor([FromQuery] Guid id)
        {
            var author = unitOfWork.Authors.Get(id);
            if (author == null)
                return NotFound($"Author with Id {id} not found");
            var book = unitOfWork.Books.GetAll(b => b.AuthorId == id, includes: include);
            if (book == null)
                return NotFound();
            return Ok( book.Select( b => MapToBookDTO(b)));
        }

        [HttpGet]
        public IActionResult GetBooksByTitle([FromQuery] string title)
        {
            var book = unitOfWork.Books.GetAll(b => b.Title.ToLower().Contains(title.ToLower()), includes: include);
            if (book == null)
                return NotFound();
            return Ok(book.Select(b => MapToBookDTO(b)));
        }
        [HttpGet]
        public IActionResult GetBooksByGenre([FromQuery] Guid id)
        {
            var genre = unitOfWork.Genres.Get(id);
            if (genre == null)
                return NotFound($"Genre with Id {id} not found");
            var book = unitOfWork.Books.GetAll(b => b.GenreId == id, includes: include);
            if (book == null)
                return NotFound();
            return Ok(book.Select(b => MapToBookDTO(b)));
        }

        [HttpGet]
        public IActionResult GetBooksByYear([FromQuery] int year)
        {
            var book = unitOfWork.Books.GetAll(b => b.Year == year, includes: include);
            if (book == null)
                return NotFound();
            return Ok(book.Select(b => MapToBookDTO(b)));
        }

        [HttpPost]
        public IActionResult DeleteBook([FromBody] BookDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var checkResult = BasicChecks(dto);
            if (checkResult != null)
                return NotFound(checkResult);
            var book = unitOfWork.Books.Get(dto.Id);
            if (book == null)
                return NotFound($"Book with Id {dto.Id} not found");
            unitOfWork.Books.Remove(MapToBook(dto, book));
            return Ok();
        }

        private string? BasicChecks(BookDTO dto)
        {
            Guid authorId = dto.AuthorId;
            var author = unitOfWork.Authors.Get(authorId);
            if (author == null)
                return "Author not found";
            Guid genreId = dto.GenreId;
            var genre = unitOfWork.Genres.Get(authorId);
            if (genre == null)
                return "Genre not found";
            return null;
        }

        public static Book MapToBook(BookDTO dto, Book? model)
        {
            //model should only be null during creation of new item
            return new Book(dto.Name)
            {
                Id = model == null ? new Guid() :model.Id,
                AuthorId = model == null ? dto.AuthorId : model.AuthorId,
                GenreId = model == null ? dto.GenreId : model.GenreId,
                ISBN = model == null ? dto.ISBN : model.ISBN,
                Price = model == null ? dto.Price : model.Price,
                Year = model == null ? dto.Year : model.Year,
            };
        }
        public static BookDTO MapToBookDTO(Book model)
        {
            return new BookDTO
            {
                Id = model.Id,
                AuthorId = model.AuthorId,
                AuthorName = model.Author?.Name,
                GenreId = model.GenreId,
                GenreName = model.Genre?.Name,
                Price = model.Price,
                Name = model.Name,
                Year = model.Year,
                Title = model.Title           
            };
        }
    }
}
