using AFI.BusinessLogic.Entities;
using AFI.Models.Client;

namespace AFI.Models.Adapters
{
    public abstract class BaseAdapter<TClientModel, TEntity>
        : IAdapter<TClientModel, TEntity>
        where TClientModel : class, IClientModel, new()
        where TEntity : class, IBaseEntity, new()
    {
        protected BaseAdapter()
        {
        }
        public abstract TClientModel ToModel(TEntity entity);

        public TEntity ToEntity(TClientModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            TEntity entity = new TEntity();

            TEntity result = ToEntity(model, entity);

            return result;
        }

        public abstract TEntity ToEntity(TClientModel model, TEntity entity);
    }
}
