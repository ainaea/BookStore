using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class CartedBook : Identifiable
    {
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;
        public Guid BookId { get; set; }
        public Guid CartId { get; set; }
        public virtual Book? Book { get; set; }        
    }
}
