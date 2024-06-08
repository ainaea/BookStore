using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration config;

        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options, IConfiguration config) : base(options)
        {
            this.config = config;
        }

        public DbSet<Author> Authors { get; set; }
            public DbSet<Book> Books { get; set; }
            public DbSet<Cart> Carts { get; set; }
            public DbSet<CartedBook> CartedBooks { get; set; }
            public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(config.GetConnectionString("BookStoreConnection"));
        }
        //public static async Task SeedDataAsync(BookStoreDbContext context)
        //{
        //    string superMail = "super@admin.com";
        //    var user = context.Users.FirstOrDefault(u => u.Email == superMail);
        //    if (user != null)
        //        return;

        //    var role = new IdentityRole { Name = "superadmin", NormalizedName = "SUPER ADMIN" };
        //    context.Roles.Add(role);
        //    await context.SaveChangesAsync();

        //    user = new IdentityUser
        //    {
        //        UserName = "SuperAdmindmin",
        //        Email = superMail,
        //        EmailConfirmed = true,
        //        PasswordHash = "<password_hash>"
        //    };

        //    context.Users.Add(user);
        //    await context.SaveChangesAsync();

        //    var userId = user.Id;
        //    var roleId = role.Id;
        //    var userRole = new IdentityUserRole<string> { UserId = userId, RoleId = roleId };
        //    context.UserRoles.Add(userRole);
        //    await context.SaveChangesAsync();

        //}


    }
}
