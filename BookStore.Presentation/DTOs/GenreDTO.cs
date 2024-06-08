using BookStore.Domain.Entities;

namespace BookStore.Presentation.DTOs
{
    public class GenreDTO: BaseDTO
    {
        public virtual IEnumerable<BookDTO>? Books { get; set; }
    }
}
