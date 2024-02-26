using AFI.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace AFI.DataAccess.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class, IBaseEntity
    {
        internal readonly DbContext context;
        internal readonly DbSet<TEntity> dbSet;

        protected Repository(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await dbSet.FindAsync(id);

            if (entity == null)
                return false;

            dbSet.Remove(entity);

            return true;
        }

        public async Task<TEntity> GetById(int id)
        {
            TEntity result = await dbSet.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetList()
        {
            return await dbSet.ToListAsync();
        }


        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
