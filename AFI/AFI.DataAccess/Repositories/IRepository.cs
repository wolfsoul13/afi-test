using AFI.BusinessLogic.Entities;

namespace AFI.DataAccess.Repositories
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class, IBaseEntity
    {
        Task<TEntity> Insert(TEntity entity);
        Task<bool> Delete(int id);
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> GetList();
        void Update(TEntity entity);
        Task Save();
    }
}
