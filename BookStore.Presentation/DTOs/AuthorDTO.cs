using BookStore.Domain.Entities;
using BookStore.Presentation.DTOs;

namespace BookStore.Presentation.ViewModels
{
    public class AuthorDTO: BaseDTO
    {
        public virtual IEnumerable<BookDTO>? Publications { get; set; }
    }
}
