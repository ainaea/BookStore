using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Presentation.DTOs
{
    public class BookDTO: BaseDTO
    {
        public string Title { get => Name; set { Name = value; } }
        [Range(1000, int.MaxValue)]
        public int Year { get; set; }
        public string? ISBN { get; set; }
        public string? AuthorName { get; set; }

        public string? GenreName { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public Guid GenreId { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
    }
}
