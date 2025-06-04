using BitTech.Vendas.Api.Domain.Models;
using System.Linq.Expressions;

namespace BitTech.Vendas.Api.Domain.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : Entity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> GetAsync(Func<TEntity, bool> where);
    Task<TEntity> GetByIdAsync(Guid id);
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}