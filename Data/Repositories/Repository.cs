using CandidateHub.Data.Contexts;
using CandidateHub.Data.IRepositories;
using CandidateHub.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CandidateHub.Data.Repositories;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly AppDbContext dbContext;
    private readonly DbSet<TEntity> dbSet;

    public Repository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.dbSet = dbContext.Set<TEntity>();
    }

    public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await this.SelectAsync(expression);
        if (entity is not null)
        {
            this.dbSet.Remove(entity);
            return true;
        }
        return false;

    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var entry = await this.dbSet.AddAsync(entity);
        return entry.Entity;
    }

    public async Task SaveAsync()
    {
        await this.dbContext.SaveChangesAsync();
    }

    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null)
    {
        IQueryable<TEntity> query = expression is null ? this.dbSet : this.dbSet.Where(expression);

        if (includes is not null)
        {
            foreach (string include in includes)
            {
                query = query.Include(include);
            }
        }

        return query;
    }

    public async Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null)
        => await this.SelectAll(expression, includes).FirstOrDefaultAsync();

    public TEntity Update(TEntity entity)
    {
        var entry = this.dbSet.Update(entity);
        return entry.Entity;
    }
}