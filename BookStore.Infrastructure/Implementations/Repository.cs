using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using Dapper;
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
        private readonly SqlConnectionFactory connectionFactory;
        private DbSet<T> dbSet;
        public Repository(BookStoreDbContext dbContext, SqlConnectionFactory connectionFactory)
        {
            this.dbContext = dbContext;
            this.connectionFactory = connectionFactory;
            dbSet = dbContext.Set<T>();
        }
        public async Task Add(T entity)
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync($"INSERT INTO {GetClassName()} ({string.Join(", ", typeof(T).GetProperties().Select(p => p.Name))}) VALUES ({string.Join(", ", typeof(T).GetProperties().Select(p => $"@{p.Name}"))})", entity);
            }

        }

        public T? Get(Guid id)
        {
            //return await dbSet.FindAsync(id);
            using (var connection = connectionFactory.CreateConnection())
            {
                return connection.QueryFirstOrDefault<T>($"SELECT * FROM {GetClassName()} WHERE {nameof(Identifiable.Id)} = @id", new { id });
            }
        }
        public IEnumerable<T>? GetAll()
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                return connection.Query<T>($"SELECT * FROM {GetClassName()}");
            }
        }
        public T? Get(Expression<Func<T, bool>> filter, string[]? includes)
        {
            return GetAll()?.AsQueryable<T>().Where(filter).FirstOrDefault();
        }

        public IEnumerable<T>? GetAll(Expression<Func<T, bool>> filter, string[]? includes = null, int count = int.MaxValue)
        {
            IQueryable<T>? query = GetAll()?.AsQueryable<T>();
            return query?.Where(filter)?.Take(count).ToList();
        }        

        public void Remove(T entity)
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                connection.Execute($"DELETE FROM {GetClassName()} WHERE {nameof(Identifiable.Id)} = @id", new { entity.Id });
            }

        }

        public void Update(T entity)
        {
            using (var connection = connectionFactory.CreateConnection())
            {
                var updateQuery = $"UPDATE {GetClassName()} SET ";
                var parameters = new DynamicParameters();
                parameters.AddDynamicParams(entity);
                foreach (var property in typeof(T).GetProperties())
                {
                    updateQuery += $"{property.Name} = @{property.Name}, ";
                    //parameters.AddDynamicParameter(property.Name, property.GetValue(entity));
                }

                updateQuery = updateQuery.TrimEnd(' ', ',');
                updateQuery += $" WHERE {nameof(Identifiable.Id)} = {entity.Id}";

                connection.Execute(updateQuery, parameters);
            }

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
        private string GetClassName() =>  typeof(T).Name+"s";
    }
}
