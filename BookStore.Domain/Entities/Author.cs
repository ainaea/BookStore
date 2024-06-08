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
        public Author(string name) : base(name)
        {
            Publications = new List<Book>();
        }
        public Author(): base(string.Empty)
        {
            Publications = new List<Book>();
        }        
    }
}
