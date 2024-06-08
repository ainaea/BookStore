using BookStore.Application.Interfaces;
using BookStore.Infrastructure;
using BookStore.Infrastructure.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using System;

namespace Infrastructure.Setup
{
    public class InfrastructureSetup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {            
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<BookStoreDbContext>()
                    .AddDefaultTokenProviders();            
            services.AddDbContextPool<BookStoreDbContext>(options => options.UseNpgsql(config.GetConnectionString("BookStoreConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<BookStoreDbContext, BookStoreDbContext>();
            services.AddSingleton<SqlConnectionFactory, SqlConnectionFactory>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IPaymentService, PaymentService>();
        }        
    }
}
