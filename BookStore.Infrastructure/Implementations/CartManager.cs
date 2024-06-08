using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Implementations
{
    public class CartManager : ICartManager
    {
        public decimal GetCartTotal(Cart cart)
        {
            if (cart == null)
                return default;
            if (cart.CartedBooks == null || !cart.CartedBooks.Any())
                return default;
            if (cart.CartedBooks.Any( cb => cb.Book == null) )
                return default;
            decimal total = 0;
            foreach (var cartedBook in cart.CartedBooks)
                total += (cartedBook.Quantity * cartedBook?.Book?.Price)!.Value;

            return total;
        }
    }
}
