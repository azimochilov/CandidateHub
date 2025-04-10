using CandidateHub.Domain.Commons;
using System.Linq.Expressions;

namespace CandidateHub.Data.IRepositories;
public interface IRepository<TEntity> where TEntity: Auditable
{
    Task<TEntity> InsertAsync(TEntity entity);
    TEntity Update(TEntity entity);
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null);
    IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null);
    Task SaveAsync();
}
