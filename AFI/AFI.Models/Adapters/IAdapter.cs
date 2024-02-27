
using AFI.BusinessLogic.Entities;
using AFI.Models.Client;

namespace AFI.Models.Adapters
{
    public interface IAdapter<TClientModel, TEntity>
        where TClientModel : class, IClientModel
        where TEntity : class, IBaseEntity
    {
        TClientModel ToModel(TEntity entity);
        TEntity ToEntity(TClientModel model);
        TEntity ToEntity(TClientModel model, TEntity entity);
    }
}
