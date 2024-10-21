using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Domain.Result;
using System.Linq.Expressions;


namespace Clinicaapp.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<OperationResult> Save(TEntity entity);
        Task<OperationResult> Update(TEntity entity);
        Task<OperationResult> Remove(TEntity entity);
        Task<OperationResult> GetAll();
        Task<OperationResult> GetAll(Expression<Func<TEntity, bool>> filter);
        Task<OperationResult> GetEntityBy(int Id);
        Task<bool> Exists(Expression<Func<TEntity, bool>> filter);

    }
}
