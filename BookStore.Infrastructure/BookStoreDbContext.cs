using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure
{
    public class BookStoreDbContext : IdentityDbContext<IdentityUser>
    {        
            public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
            { }

            public DbSet<Author> Authors { get; set; }
            public DbSet<Book> Books { get; set; }
            public DbSet<Cart> Carts { get; set; }
            public DbSet<CartedBook> CartedBooks { get; set; }
            public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);
                //builder.Entity<OneLoan>().Property(o => o.InterestRate).HasColumnType("decimal(18,2)");
                //builder.Entity<OneLoan>().Property(o => o.Amount).HasColumnType("decimal(18,2)");
            }
    }
}
