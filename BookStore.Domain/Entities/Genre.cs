using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class Genre : Nameable
    {
        public virtual IEnumerable<Book> Books { get; set; }
        public Genre(string name) : base(name)
        {
            Books = new List<Book>();
        }
    }
}
