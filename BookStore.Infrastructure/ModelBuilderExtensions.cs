using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author("K. J, Hang") { Id = new Guid("3fcd7f7e-5417-4e68-9d3a-74c522477645") },
                new Author("John, Stones") { Id = new Guid("817a54a1-94a3-45af-9d3b-5a74423f645a") }, 
                new Author("F, Mustang") { Id = new Guid("2f5b197f-5a93-4f1e-a35b-5a74423f645b") }
                );

            modelBuilder.Entity<Genre>().HasData(
                new Genre("Politics") { Id = new Guid("9c20994a-9475-4974-9d3c-5a74423f645c") },
                new Genre("Sport") { Id = new Guid("8e374268-5a3f-4f1e-a35b-5a74423f645d") },
                new Genre("Automobile") { Id = new Guid("9d352541-9475-4974-9d3c-5a74423f645e") }
                );

            modelBuilder.Entity<Book>().HasData(
                new Book("China, A marvel to behold") { Id = new Guid("5a3f4e7e-5417-4e68-9d3a-74c522477645"), AuthorId = new Guid("3fcd7f7e-5417-4e68-9d3a-74c522477645"), GenreId = new Guid("9c20994a-9475-4974-9d3c-5a74423f645c") },
                new Book("The making of 4 in-a-row") { Id = new Guid("4f1e4e7e-5417-4e68-9d3a-74c522477645"), AuthorId = new Guid("817a54a1-94a3-45af-9d3b-5a74423f645a"), GenreId = new Guid("8e374268-5a3f-4f1e-a35b-5a74423f645d") },
                new Book("800Hp, Insane?") { Id = new Guid("2a3f4e7e-5417-4e68-9d3a-74c522477645"), AuthorId = new Guid("2f5b197f-5a93-4f1e-a35b-5a74423f645b"), GenreId = new Guid("9d352541-9475-4974-9d3c-5a74423f645e") }
                );
        }
    }
}
