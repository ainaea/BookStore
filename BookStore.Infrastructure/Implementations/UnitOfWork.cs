using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Book> Books { get;}

        public IRepository<Author> Authors { get; }
        public IRepository<Genre> Genres { get; }
        public IRepository<Cart> Carts { get; }

        public IRepository<CartedBook> CartedBooks { get; }
        public UnitOfWork(IRepository<Book> books, IRepository<Author> authors, IRepository<Cart> carts, IRepository<CartedBook> cartedBooks, IRepository<Genre> genres)
        {
            Books = books;
            Authors = authors;
            Carts = carts;
            CartedBooks = cartedBooks;
            Genres = genres;
        }

        public Task<int> Complete()
        {
            return Task.FromResult(1);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
