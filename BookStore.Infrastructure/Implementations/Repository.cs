using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Implementations
{
    public class Repository<T> : IRepository<T> where T : Identifiable
    {
        private readonly BookStoreDbContext dbContext;
        private DbSet<T> dbSet;
        public Repository(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }
        public async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<T?> Get(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<T?> Get(Expression<Func<T, bool>> filter, string[]? includes)
        {
            IQueryable<T> query = GetQuerry(includes);
            return await query.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll(string[]? includes = null, int count = 20)
        {
            IQueryable<T> query = GetQuerry(includes);
            return await query.Take(count).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter, string[]? includes = null)
        {
            IQueryable<T> query = GetQuerry(includes);
            return await query.Where(filter).ToListAsync();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
        private IQueryable<T> GetQuerry(string[]? includes)
        {
            IQueryable<T> query = dbSet;
            if (includes != null)
            {
                foreach (var child in includes)
                {
                    query = query.Include(child);
                }
            }
            return query;
        }
    }
}
