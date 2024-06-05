using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        public IRepository<Book> Books { get; }
        public IRepository<Author> Authors { get; }
        public IRepository<Cart> Carts { get; }
        public IRepository<CartedBook> CartedBooks { get; }
        Task<int> Complete();
    }
}
