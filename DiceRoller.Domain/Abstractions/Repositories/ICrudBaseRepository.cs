using System.Linq.Expressions;
using DiceRoller.Domain.Abstractions.Pagination;
using DiceRoller.Domain.Entities;

namespace DiceRoller.Domain.Abstractions.Repositories
{
    public interface ICrudBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> FindAllAsync(PaginationOptions? request);
        Task<IEnumerable<TEntity>> FindWhereAsync(Expression<Func<TEntity, bool>> whereExpression, PaginationOptions? request);
        Task<PaginatedResult<TEntity>> GetPagedAsync(PaginationOptions request, Expression<Func<TEntity, bool>>[] whereExpressions);
        Task<PaginatedResult<TEntity>> GetPagedAsync(PaginationOptions request, Expression<Func<TEntity, bool>> whereExpressions);
        Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereExpression);
        Task<int> CountAllAsync();
        Task<int> CountWhereAsync(Expression<Func<TEntity, bool>> whereExpression);
        Task<TEntity?> GetByIdAsync(Guid id, bool includeDeleted = false);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity?> UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
    }
}
