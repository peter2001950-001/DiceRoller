using System.Linq.Expressions;
using DiceRoller.Domain.Abstractions.Pagination;
using DiceRoller.Domain.Abstractions.Repositories;
using DiceRoller.Domain.Entities;
using DiceRoller.Infrastructure.Data;
using DiceRoller.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace DiceRoller.Infrastructure.Repositories
{
    public abstract class CrudBaseRepository<TEntity> : ICrudBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DiceDbContext DbContext;

        protected CrudBaseRepository(DiceDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        protected abstract IQueryable<TEntity> Query { get; }

        public async Task<IEnumerable<TEntity>> FindAllAsync(PaginationOptions? request)
        {
            return await Query.Sorted(request).Paged(request).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindWhereAsync(Expression<Func<TEntity, bool>> whereExpression, PaginationOptions? request)
        {
            return await Query.Where(whereExpression).Sorted(request).Paged(request).ToListAsync();
        }

        public async Task<PaginatedResult<TEntity>> GetPagedAsync(PaginationOptions request, Expression<Func<TEntity, bool>>[] whereExpressions)
        {
            var items = Query;
            foreach (var whereExpression in whereExpressions)
            {
                items = Query.Where(whereExpression);
            }

            var pagedItems = await items.Sorted(request).Paged(request).ToListAsync();
            var totalCount = await items.CountAsync();

            return new PaginatedResult<TEntity>(request, totalCount, pagedItems);
        }

        public async Task<PaginatedResult<TEntity>> GetPagedAsync(PaginationOptions request, Expression<Func<TEntity, bool>> whereExpressions)
        {
            var items = Query.Where(whereExpressions);
            var pagedItems = await items.Sorted(request).Paged(request).ToListAsync();
            var totalCount = await items.CountAsync();

            return new PaginatedResult<TEntity>(request, totalCount, pagedItems);
        }

        public async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Query.Where(whereExpression).FirstOrDefaultAsync();
        }

        public async Task<int> CountAllAsync()
        {
            return await Query.CountAsync();
        }

        public async Task<int> CountWhereAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Query.Where(whereExpression).CountAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id, bool includeDeleted = false)
        {
            if (includeDeleted)
                return await Query.FirstOrDefaultAsync(x => x.Id == id);
            return await Query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (await CanAddAsync(entity))
            {
                entity = await BeforeAddAsync(entity);
                entity.CreatedOn = DateTime.UtcNow;
                await DbContext.AddAsync(entity);
                await DbContext.SaveChangesAsync();
                await AfterAddAsync(entity);
            }

            return entity;
        }

        public virtual async Task<TEntity?> UpdateAsync(TEntity entity)
        {
            if (await CanUpdateAsync(entity))
            {
                var localEntity = await Query.AsNoTracking().FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (localEntity == null)
                    return null;

                entity = await BeforeUpdateAsync(entity.Id, entity, localEntity);

                DbContext.Update(entity);
                await DbContext.SaveChangesAsync();
                await AfterUpdateAsync(entity);
            }

            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                return;

            if (await CanDeleteAsync(entity))
            {
                DbContext.Remove(entity);
                await DbContext.SaveChangesAsync();
            }
        }

        protected virtual Task<bool> CanDeleteAsync(TEntity entity)
        {
            return Task.FromResult(true);
        }

        protected virtual Task<bool> CanAddAsync(TEntity entity)
        {
            return Task.FromResult(true);
        }

        protected virtual Task<bool> CanUpdateAsync(TEntity entity)
        {
            return Task.FromResult(true);
        }

        protected virtual Task<TEntity> BeforeUpdateAsync(Guid id, TEntity entity, TEntity localEntity)
        {
            return Task.FromResult(entity);
        }

        protected virtual Task<TEntity> BeforeAddAsync(TEntity entity)
        {
            return Task.FromResult(entity);
        }

        protected virtual Task<TEntity> AfterAddAsync(TEntity entity)
        {
            return Task.FromResult(entity);
        }

        protected virtual Task<TEntity> AfterUpdateAsync(TEntity entity)
        {
            return Task.FromResult(entity);
        }
    }
}
