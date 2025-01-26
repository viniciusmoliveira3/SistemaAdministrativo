namespace Colex.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Delete(int id);
        void Update(TEntity entity);
        TEntity? GetById(int id);
        List <TEntity> GetAll();
        void AddRange(List<TEntity> entities);
    }
}
