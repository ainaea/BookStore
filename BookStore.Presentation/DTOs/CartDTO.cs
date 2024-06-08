using BookStore.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Presentation.DTOs
{
    public class CartDTO : BaseDTO
    {
        [Required]
        public Guid UserId { get; set; }
        public virtual IEnumerable<CartedBook>? CartedBooks { get; set; }
        public PaymentEnum PaymentStatus { get; set; } = PaymentEnum.Pending;
    }
}
