using BookStore.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Presentation.DTOs
{
    public class CartedBookDTO : BaseDTO
    {
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;
        [Required]
        public Guid BookId { get; set; }
        [Required]
        public Guid CartId { get; set; }
        public virtual Book? Book { get; set; }        
    }
}
