using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Implementations;

namespace Bookstore.Test
{
    public class CartManagerTests
    {
        private CartManager cartManager;
        public CartManagerTests()
        {
            cartManager = new CartManager();
        }
        [Fact]
        public void GetCartTotal_NullCart_ReturnsDefault()
        {
            //var cartManager = new CartManager();
            Cart cart = null!;

            var result = cartManager.GetCartTotal(cart);

            Assert.Equal(default(decimal), result);
        }
        [Fact]
        public void GetCartTotal_EmptyCart_ReturnsDefault()
        {
            //var cartManager = new CartManager();
            var cart = new Cart { CartedBooks = new List<CartedBook>() };

            var result = cartManager.GetCartTotal(cart);

            Assert.Equal(default(decimal), result);
        }

        [Fact]
        public void GetCartTotal_CartWithNullBooks_ReturnsDefault()
        {
            //var cartManager = new CartManager();
            var cart = new Cart
            {
                CartedBooks = new List<CartedBook>
                {
                    new CartedBook { Book = null }
                }
            };

            var result = cartManager.GetCartTotal(cart);

            Assert.Equal(default(decimal), result);
        }
        [Fact]
        public void GetCartTotal_CartWithBooks_ReturnsTotal()
        {
            //var cartManager = new CartManager();
            var cart = new Cart
            {
                CartedBooks = new List<CartedBook>
                {
                    new CartedBook { Quantity = 2, Book = new Book { Price = 10.99m } },
                    new CartedBook { Quantity = 3, Book = new Book { Price = 5.99m } }
                }
            };

            var result = cartManager.GetCartTotal(cart);

            Assert.Equal(39.95m, result);
        }




    }
}