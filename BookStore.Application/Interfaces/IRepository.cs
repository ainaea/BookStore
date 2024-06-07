using BookStore.Domain.Entities;
using System.Linq.Expressions;

namespace BookStore.Application.Interfaces
{
    public interface IRepository<T> where T: Identifiable
    {
        T? Get(Guid id);
        T? Get(Expression<Func<T, bool>> filter, string[]? includes = null);
        IEnumerable<T>? GetAll(Expression<Func<T, bool>> filter, string[]? includes = null, int count = 20);
        //Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter, string[]? includes = null);
        IEnumerable<T>? GetAll(object[]? includes = null);
        Task Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}
