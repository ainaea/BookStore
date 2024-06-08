using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Presentation.DTOs;
using BookStore.Presentation.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public GenreController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route($"{nameof(GetAll)}")]
        public IActionResult GetAll()
        {
            return Ok(unitOfWork.Genres.GetAll()?.Select(a => MapToGenreDTO(a)));
        }
        [HttpPost]
        [Route($"{nameof(AddGenre)}")]
        public IActionResult AddGenre([FromBody] GenreDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(unitOfWork.Genres.Add(MapToGenre(dto, null)));
        }
        [HttpPost]
        [Route($"{nameof(EditGenre)}")]
        public IActionResult EditGenre([FromBody] GenreDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var genre = unitOfWork.Genres.Get(dto.Id);
            if (genre == null)
                return NotFound($"Genre with Id {dto.Id} not found");
            unitOfWork.Genres.Update(MapToGenre(dto, genre));
            return Ok();
        }
        [HttpGet]
        public IActionResult GetGenre([FromQuery] Guid id)
        {
            var genre = unitOfWork.Genres.Get(id);
            if (genre == null)
                return NotFound();
            return Ok(MapToGenreDTO(genre));
        }
        [HttpPost]
        [Route($"{nameof(DeleteGenre)}")]
        public IActionResult DeleteGenre([FromBody] GenreDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var genre = unitOfWork.Genres.Get(dto.Id);
            if (genre == null)
                return NotFound($"Genre with Id {dto.Id} not found");
            unitOfWork.Genres.Remove(MapToGenre(dto, genre));
            return Ok();
        }
        public static Genre MapToGenre (GenreDTO dto, Genre? model)
        {
            return new Genre(dto.Name)
            {
                Id = model == null ? new Guid() : model.Id,
            };
        }
        public static GenreDTO MapToGenreDTO(Genre model)
        {
            return new GenreDTO
            {
                Id = model.Id,
                Name = model.Name,
                Books = model.Books?.Select(x => BookController.MapToBookDTO(x))
            };
        }
    }
}
