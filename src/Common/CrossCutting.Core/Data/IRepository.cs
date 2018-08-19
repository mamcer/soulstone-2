namespace CrossCutting.Core.Data
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        TEntity Get(TKey id);

        TEntity Update(TEntity entity);

        TEntity Save(TEntity entity);

        void Delete(TEntity entity);
    }
}