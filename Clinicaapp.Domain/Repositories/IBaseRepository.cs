namespace Clinicaapp.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<OperationResult> Save(TEntity entity);
        Task<OperationResult> Update(TEntity entity);
        Task<OperationResult> Remove(TEntity entity);
        Task<OperationResult> GetAll();
        Task<OperationResult> GetEntityBy(int Id);
        Task<OperationResult> Exists(Expression <Func<TEntity, bool>> filter);
    }
}
