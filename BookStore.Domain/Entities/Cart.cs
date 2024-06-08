using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class Cart : Identifiable
    {
        public Cart(Guid userId)
        {
            UserId = userId;
        }
        public Guid UserId { get; private set; }
        public virtual IEnumerable<CartedBook>? CartedBooks { get; set; }
        public PaymentEnum PaymentStatus { get; set; } = PaymentEnum.Pending;
        public Cart()
        {
            
        }

    }
}
