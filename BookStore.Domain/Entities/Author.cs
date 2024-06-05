using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class Author : Nameable
    {
        public virtual IEnumerable<Book> Publications { get; set; }
        public Author(string fullname) : base(fullname)
        {
            Publications = new List<Book>();
        }
    }
}
