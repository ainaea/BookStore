using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class Book : Nameable
    {
        [NotMapped]
        public string Title { get => Name; set { Name = value; } }
        [Range(1000, int.MaxValue)]
        public int Year { get; set; }
        public string? ISBN { get; set; }
        public virtual Author? Author { get; set; }

        public virtual Genre? Genre { get; set; }
        public Guid AuthorId { get; set; }
        public Guid GenreId { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        public Book(string name) : base(name)
        {

        }
    }
}
