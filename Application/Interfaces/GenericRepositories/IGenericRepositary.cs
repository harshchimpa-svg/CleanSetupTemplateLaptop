using Application.Dto.Locations;
using Domain.Common.Interfaces;
using System.Linq.Expressions;

namespace Application.Interfaces.GenericRepositories;

public interface IGenericRepository<T> where T : class, IEntity
{
    IQueryable<T> Entities { get; }
    Task<List<T>> GetAll();
    Task<T?> GetByID(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity, int id=0);
    Task DeleteAsync(T entity);
    Task DeleteAsync(int id);
    Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector);
}
