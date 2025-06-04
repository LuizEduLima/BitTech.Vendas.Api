using BitTech.Vendas.Api.Domain.Interfaces;
using BitTech.Vendas.Api.Domain.Models;
using System.Linq.Expressions;

namespace BitTech.Vendas.Api.Data.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
{
    protected readonly List<TEntity> _entities = [];

    public virtual Task<IEnumerable<TEntity>> GetAllAsync()
    => Task.FromResult(_entities.AsEnumerable());

    public virtual Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var compiledPredicate = predicate.Compile();
        var result = _entities.Where(compiledPredicate);
        return Task.FromResult(result);
    }

    public virtual Task<TEntity> GetAsync(Func<TEntity, bool> where)
    {
        var entity = _entities.FirstOrDefault(where);
        return Task.FromResult(entity);
    }

    public virtual Task<TEntity> GetByIdAsync(Guid id)
    {
        var entity = _entities.FirstOrDefault(e => e.Id == id);
        return Task.FromResult(entity);
    }

    public virtual Task<TEntity> CreateAsync(TEntity entity)
    {
        _entities.Add(entity);
        return Task.FromResult(entity);
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity)
    {
        var existingEntity = _entities.FirstOrDefault(e => e.Id == entity.Id);

        if (existingEntity is null)
            throw new InvalidOperationException($"Entidade com ID {entity.Id} não encontrada.");

        var index = _entities.IndexOf(existingEntity);
        _entities[index] = entity;

        return Task.FromResult(entity);
    }

    public virtual Task<bool> DeleteAsync(Guid id)
    {
        var entity = _entities.FirstOrDefault(e => e.Id == id);
        if (entity == null)
            return Task.FromResult(false);

        _entities.Remove(entity);
        return Task.FromResult(true);
    }

    public virtual Task<bool> ExistsAsync(Guid id)
    {
        var exists = _entities.Any(e => e.Id == id);
        return Task.FromResult(exists);
    }
}
