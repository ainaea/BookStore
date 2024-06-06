using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class Cart : Identifiable
    {
        public Guid UserId { get; set; }
        public virtual IEnumerable<CartedBook>? CartedBooks { get; set; }
        public PaymentEnum PaymentStatus { get; set; } = PaymentEnum.Pending;

    }
}
